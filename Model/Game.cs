using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalEnv.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string ExportTitle { get; set; }
        public string TesterPath { get; set; }
        public int SeedStart { get; set; }
        public int SeedCount { get; set; }
        public bool Maximize { get; set; }
        public bool Minimize => !Maximize;
        public List<Parameter> Parameters { get; set; }
        public List<Agent> Agents { get; set; }
        public List<SeedInfo> SeedInfos { get; set; }
        public string Folder => $"wwwroot/uploads/{Id}/";
        [NotMapped] public bool Running { get; set; } = true;

        public double ComputeRelativeScore(SeedInfo seedInfo, double score)
        {
            if (score == -1) return 0;
            if (Maximize)
            {
                if (score > seedInfo.BestScore)
                {
                    seedInfo.BestScore = score;
                    foreach (Agent agent in Agents) agent.UpdateSeedScore(seedInfo, this);
                    return 1;
                }
                return score / seedInfo.BestScore;
            }

            if (score < seedInfo.BestScore)
            {
                seedInfo.BestScore = score;
                foreach (Agent agent in Agents) agent.UpdateSeedScore(seedInfo, this);
                return 1;
            }
            return seedInfo.BestScore / score;
        }

        public async Task GenerateSeedInfo(int seed)
        {
            SeedInfo info = SeedInfos.FirstOrDefault(s => s.Seed == seed);
            if (info != null) return;

            info = new SeedInfo
            {
                Seed = seed,
                ParameterValues = Parameters.Select(p => new ParameterValue { Parameter = p }).ToList()
            };

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "java";
            process.StartInfo.Arguments = $"-jar {TesterPath} -debug -novis -seed {seed}";
            process.Start();
            string stdOut = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            string[] lines = stdOut.Split("\r\n".ToCharArray());
            foreach (string line in lines)
            {
                Match match = Regex.Match(line, @"(?<var>\w+) = (?<number>\d+(\.\d+)?)");
                if (!match.Success) continue;
                string variable = match.Groups["var"].Value;
                string number = match.Groups["number"].Value;
                ParameterValue value = info.ParameterValues.FirstOrDefault(v => v.Parameter.InternalName == variable);
                if (value != null) value.Value = double.Parse(number, CultureInfo.InvariantCulture);
            }
            SeedInfos.Add(info);
        }

        public void InitScores()
        {
            Dictionary<int, SeedInfo> infos = SeedInfos.ToDictionary(s => s.Seed, s => s);
            // find best absolute scores
            foreach (Agent agent in Agents)
            {
                foreach (TestcaseResult result in agent.TestcaseResults)
                {
                    SeedInfo info = infos[result.Seed];
                    if (info.BestScore == 0) info.BestScore = result.Score;
                    else if (Maximize) info.BestScore = Math.Max(info.BestScore, result.Score);
                    else info.BestScore = Math.Min(info.BestScore, result.Score);
                }
            }

            // find relative scores
            foreach (Agent agent in Agents)
            {
                foreach (TestcaseResult result in agent.TestcaseResults)
                {
                    agent.LoadTestcaseResult(infos[result.Seed], this, result);
                }
            }
        }
    }
}