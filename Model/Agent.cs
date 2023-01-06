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
    }
}