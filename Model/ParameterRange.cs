namespace LocalEnv.Model
{
    public class ParameterRange
    {
        public int Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public override string ToString() => MinValue == MaxValue ? MinValue.ToString() : MinValue + "-" + MaxValue;

        public static ParameterRange Parse(string text)
        {
            string[] parts = text.Split('-');
            ParameterRange range = new ParameterRange
            {
                MinValue = double.Parse(parts[0]),
                MaxValue = double.Parse(parts[^1]),
            };
            return range;
        }
    }
}