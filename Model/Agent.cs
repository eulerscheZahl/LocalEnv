using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace LocalEnv.Model
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string CodePath { get; set; }
        public string BinaryPath { get; set; }
        public List<TestcaseResult> TestcaseResults { get; set; }

        [NotMapped] public bool Running { get; set; } = true;
        public string ExecuteCommand()
        {
            if (Language == "C#") return "dotnet " + Directory.GetCurrentDirectory() + "/" + BinaryPath;
            if (Language == "C++") return Directory.GetCurrentDirectory() + "/" + BinaryPath;
            if (Language == "Java") return "java -jar " + Directory.GetCurrentDirectory() + "/" + BinaryPath + ".jar";
            return "python3 " + Directory.GetCurrentDirectory() + "/" + BinaryPath;
        }
        public double Progress(int testcases) => 100.0 * TestcaseResults.Count / testcases;
        public double Score => 100 * totalScore / Math.Max(relativeScorePerSeed.Count, 1);
        public double RangeScore(ParameterRange range)
        {
            if (!totalScoreByRange.ContainsKey(range)) return 0;
            return 100 * totalScoreByRange[range] / totalCasesByRange[range];
        }

        private Dictionary<long, double> relativeScorePerSeed = new();
        private Dictionary<long, double> absoluteScorePerSeed = new();
        private double totalScore = 0;
        private Dictionary<ParameterRange, double> totalScoreByRange = new();
        private Dictionary<ParameterRange, int> totalCasesByRange = new();

        public string CodeExtension()
        {
            return Language switch
            {
                "C#" => ".cs",
                "C++" => ".cpp",
                "Java" => ".java",
                _ => ".py"
            };
        }

        public async Task Compile(Game game)
        {
            string binaryDir = game.Folder + this.Id + "/binary/";
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetDirectoryName(binaryDir)));
            string tmpDir = Path.GetTempPath() + Guid.NewGuid() + "/";
            Directory.CreateDirectory(tmpDir);
            Directory.CreateDirectory(binaryDir);
            if (Language == "C#")
            {
                File.WriteAllText(tmpDir + "project.csproj",
@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
</Project>");
                File.WriteAllText(tmpDir + "code.cs", await BundleCode());
                this.BinaryPath = binaryDir + "project.dll";
                Process process = Process.Start("dotnet", "publish " + tmpDir + "project.csproj" + " -c Release -o " + Directory.GetCurrentDirectory() + "/" + Path.GetDirectoryName(BinaryPath));
                await process.WaitForExitAsync();
            }
            else if (Language == "C++")
            {
                File.WriteAllText(tmpDir + "code.cpp", await BundleCode());
                this.BinaryPath = binaryDir + "code.exe";
                Process process = Process.Start("g++", "--std=c++17 -O3 -o " + Directory.GetCurrentDirectory() + "/" + BinaryPath + " " + tmpDir + "code.cpp");
                await process.WaitForExitAsync();
            }
            else if (Language == "Java")
            {
                File.WriteAllText(tmpDir + game.ExportTitle + ".java", await BundleCode());
                this.BinaryPath = binaryDir + game.ExportTitle;
                binaryDir = Directory.GetCurrentDirectory() + "/" + binaryDir;
                File.WriteAllText(BinaryPath + ".mf", "Manifest-Version: 1.0\nMain-Class: " + game.ExportTitle + "\n");
                Process process = Process.Start("javac", "-d " + Directory.GetCurrentDirectory() + "/" + Path.GetDirectoryName(BinaryPath) + " " + tmpDir + game.ExportTitle + ".java");
                await process.WaitForExitAsync();
                List<string> classes = new DirectoryInfo(binaryDir).GetFiles("*.class").Select(f => f.Name).ToList();
                process = Process.Start(new ProcessStartInfo { FileName = "jar", WorkingDirectory = binaryDir, Arguments = $"cfm {game.ExportTitle}.jar {game.ExportTitle}.mf " + string.Join(" ", classes) });
                await process.WaitForExitAsync();
            }
            else
            {
                this.BinaryPath = binaryDir + "code.py";
                File.WriteAllText(this.BinaryPath, await BundleCode());
            }
        }

        public async Task<string> BundleCode()
        {
            using ZipArchive archive = ZipFile.Open(CodePath, ZipArchiveMode.Read);
            HashSet<string> usings = new();
            List<string> code = new();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                StreamReader reader = new StreamReader(entry.Open());
                string content = await reader.ReadToEndAsync();
                if (Language != "C#") return content; // only support single file
                bool inUsings = true;
                code.Add("");
                code.Add("");
                foreach (string line in content.Split("\r\n".ToCharArray()))
                {
                    if (!line.StartsWith("using")) inUsings = false;
                    if (inUsings) usings.Add(line);
                    else code.Add(line);
                }
            }
            code.InsertRange(0, usings.OrderBy(u => u));
            return string.Join(Environment.NewLine, code);
        }

        public TestcaseResult Run(Game game, SeedInfo info)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory() + "/" + game.TesterPath);
            process.StartInfo.FileName = "java";
            process.StartInfo.Arguments = $"-jar \"{Path.GetFileName(game.TesterPath)}\" -printRuntime -novis -tl 35000 -seed {info.Seed} -exec \"{this.ExecuteCommand()}\"";
            process.Start();
            string stdOut = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            string testcaseInfo = null;
            int time = 0;
            double score = -1;
            foreach (string line in stdOut.Split("\r\n".ToCharArray()))
            {
                if (line.StartsWith("TESTCASE_INFO")) testcaseInfo = line.Replace("TESTCASE_INFO", "").Trim();
                Match match = Regex.Match(line, @"Score = (?<number>\d+(\.\d+)?)");
                if (match.Success) score = double.Parse(match.Groups["number"].Value, CultureInfo.InvariantCulture);
                match = Regex.Match(line, @"RunTime = (?<number>\d+)");
                if (match.Success) time = match.Success ? int.Parse(match.Groups["number"].Value) : 0;
            }
            return new TestcaseResult { Seed = info.Seed, Score = score, Time = time, AdditionalOutput = testcaseInfo };
        }

        public void UpdateSeedScore(SeedInfo seedInfo, Game game)
        {
            if (!relativeScorePerSeed.ContainsKey(seedInfo.Seed)) return;
            double delta = -relativeScorePerSeed[seedInfo.Seed];
            relativeScorePerSeed[seedInfo.Seed] = game.ComputeRelativeScore(seedInfo, absoluteScorePerSeed[seedInfo.Seed]);
            delta += relativeScorePerSeed[seedInfo.Seed];
            totalScore += delta;
            foreach (ParameterRange range in seedInfo.Ranges)
                totalScoreByRange[range] += delta;
        }

        public void AddTestcaseResult(SeedInfo seedInfo, Game game, TestcaseResult testcaseResult)
        {
            TestcaseResults.Add(testcaseResult);
            LoadTestcaseResult(seedInfo, game, testcaseResult);
        }

        public void LoadTestcaseResult(SeedInfo seedInfo, Game game, TestcaseResult testcaseResult)
        {
            absoluteScorePerSeed[seedInfo.Seed] = testcaseResult.Score;
            relativeScorePerSeed[seedInfo.Seed] = game.ComputeRelativeScore(seedInfo, testcaseResult.Score);
            totalScore += relativeScorePerSeed[seedInfo.Seed];
            foreach (ParameterRange range in seedInfo.Ranges)
            {
                if (!totalScoreByRange.ContainsKey(range))
                {
                    totalScoreByRange[range] = 0;
                    totalCasesByRange[range] = 0;
                }
                totalScoreByRange[range] += relativeScorePerSeed[seedInfo.Seed];
                totalCasesByRange[range]++;
            }
        }
    }
}