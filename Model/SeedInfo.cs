using System.ComponentModel.DataAnnotations.Schema;

namespace LocalEnv.Model
{
    public class SeedInfo
    {
        public int Id { get; set; }
        public int Seed { get; set; }
        public List<ParameterValue> ParameterValues { get; set; }
        [NotMapped] public double BestScore { get; set; }
    }
}