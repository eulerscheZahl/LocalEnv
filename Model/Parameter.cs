namespace LocalEnv.Model
{
    public class Parameter
    {
        public int Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public List<ParameterRange> Ranges { get; set; }

        public Parameter() { }
        public Parameter(Parameter p)
        {
            this.InternalName = p.InternalName;
            this.DisplayName = p.DisplayName;
            this.Ranges = p.Ranges.Select(r => new ParameterRange(r)).ToList();
        }
    }
}