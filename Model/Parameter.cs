namespace LocalEnv.Model
{
    public class Parameter
    {
        public int Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public List<ParameterRange> Ranges { get; set; }
    }
}