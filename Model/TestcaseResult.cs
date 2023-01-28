using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LocalEnv.Model
{
    public class TestcaseResult
    {
        public int Id { get; set; }
        public long Seed { get; set; }
        public double Score { get; set; }
        public string AdditionalOutput { get; set; }
        public int Time { get; set; }

        private Dictionary<string, double> additionalValues;
        public Dictionary<string, double> AdditionalValues()
        {
            if (additionalValues != null || AdditionalOutput == null) return additionalValues ?? new();
            additionalValues = JsonSerializer.Deserialize<Dictionary<string, double>>(AdditionalOutput);
            return additionalValues;
        }
    }
}