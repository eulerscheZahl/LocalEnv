namespace LocalEnv.Model
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ParameterRange> Ranges { get; set; }
    }
}