using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class BuildTask
    {
        public string? Name { get; set; }

        public virtual ValueTask RunAsync(ILogger? logger)
        {
            return new ValueTask();
        }
    }
}
