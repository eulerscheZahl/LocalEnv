using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO.Compression;

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
        public double Score => 100 * totalScore / relativeScorePerSeed.Count;
        private Dictionary<int, double> relativeScorePerSeed = new();
        private Dictionary<int, double> absoluteScorePerSeed = new();
        private double totalScore = 0;

        public async Task Compile()
        {
            string tmpDir = Path.GetTempPath() + Guid.NewGuid() + "/";
            Directory.CreateDirectory(tmpDir);
            File.WriteAllText(tmpDir + "project.csproj",
@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
</Project>");
            File.WriteAllText(tmpDir + "code.cs", await BundleCode());
            Process process = Process.Start("dotnet", "publish " + tmpDir + "project.csproj" + " -c Release -o " + Directory.GetCurrentDirectory() + "/" + BinaryPath);
            await process.WaitForExitAsync();
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

        public void UpdateSeedScore(SeedInfo seedInfo, Game game)
        {
            totalScore -= relativeScorePerSeed[seedInfo.Seed];
            relativeScorePerSeed[seedInfo.Seed] = game.ComputeRelativeScore(seedInfo, absoluteScorePerSeed[seedInfo.Seed]);
            totalScore += relativeScorePerSeed[seedInfo.Seed];
        }

        public void AddTestcaseResult(SeedInfo seedInfo, Game game, TestcaseResult testcaseResult)
        {
            TestcaseResults.Add(testcaseResult);
            absoluteScorePerSeed[seedInfo.Seed] = testcaseResult.Score;
            relativeScorePerSeed[seedInfo.Seed] = game.ComputeRelativeScore(seedInfo, absoluteScorePerSeed[seedInfo.Seed]);
            totalScore += relativeScorePerSeed[seedInfo.Seed];
        }
    }
}