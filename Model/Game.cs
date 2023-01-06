using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalEnv.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string ExportTitle { get; set; }
        public string TesterPath { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Agent> Agents { get; set; }
    }
}