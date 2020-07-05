using System.Collections.Generic;

namespace LaTeXTools.Build.Generators
{
    public class MakeTarget
    {
        public string Name { get; set; } = "x";
        public List<string> Commands { get; set; } = new List<string>();
        public List<string> Dependencies { get; set; } = new List<string>();
        public List<string> OrderOnlyDependencies { get; set; } = new List<string>();
    }

    public class Makefile
    {
        public Dictionary<string, MakeTarget> Targets { get; set; } = new Dictionary<string, MakeTarget>();
    }
}
