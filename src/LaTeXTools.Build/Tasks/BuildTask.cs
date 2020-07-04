using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    public class BuildTask
    {
        public string? Name { get; set; }

        public virtual ValueTask RunAsync()
        {
            return new ValueTask();
        }
    }
}
