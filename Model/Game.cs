using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
    }
}