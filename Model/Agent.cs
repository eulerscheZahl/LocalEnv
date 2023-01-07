using System.ComponentModel.DataAnnotations.Schema;

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

        public void Compile()
        {

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