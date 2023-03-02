using System.ComponentModel.DataAnnotations.Schema;

namespace LocalEnv.Model
{
    public class ParameterRange
    {
        public int Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        [NotMapped] public int Testcases { get; set; }

        public override string ToString() => MinValue == MaxValue ? MinValue.ToString() : MinValue + "-" + MaxValue;

        public ParameterRange() { }
        public ParameterRange(ParameterRange range)
        {
            this.MinValue = range.MinValue;
            this.MaxValue = range.MaxValue;
        }

        public static IEnumerable<ParameterRange> Parse(string text)
        {
            int count = 1;
            if (text.Contains(":"))
            {
                count = int.Parse(text.Split(':')[1]);
                text = text.Substring(0, text.IndexOf(':'));
            }
            string[] parts = text.Split('-');
            ParameterRange range = new ParameterRange
            {
                MinValue = double.Parse(parts[0]),
                MaxValue = double.Parse(parts[^1]),
            };
            if (count == 1)
            {
                yield return range;
                yield break;
            }
            bool isInteger = int.TryParse(parts[0], out _) && int.TryParse(parts[^1], out _);
            for (int c = count; c > 0; c--)
            {
                if (isInteger)
                {
                    ParameterRange r = new ParameterRange { MinValue = range.MinValue, MaxValue = (int)(range.MinValue - 1 + (range.MaxValue + 1 - range.MinValue) / c) };
                    yield return r;
                    range.MinValue = r.MaxValue + 1;
                }
                else
                {
                    ParameterRange r = new ParameterRange { MinValue = range.MinValue, MaxValue = Math.Round(range.MinValue + (range.MaxValue - range.MinValue) / c, 3) };
                    yield return r;
                    range.MinValue = r.MaxValue;
                }
            }
            yield break;
        }

        public bool IsMatch(ParameterValue value) => value.Value >= MinValue && value.Value <= MaxValue;
    }
}