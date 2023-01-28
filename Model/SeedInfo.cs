using System.ComponentModel.DataAnnotations.Schema;

namespace LocalEnv.Model
{
    public class SeedInfo
    {
        public int Id { get; set; }
        public long Seed { get; set; }
        public List<ParameterValue> ParameterValues { get; set; }
        [NotMapped] public double BestScore { get; set; }
        [NotMapped] public List<ParameterRange> Ranges { get; set; } = new();
    }
}