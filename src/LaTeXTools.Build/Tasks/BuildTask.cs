using System;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class BuildTask : IEquatable<BuildTask>
    {
        public string? Name { get; set; }

        public virtual ValueTask RunAsync(ILogger? logger)
        {
            return new ValueTask();
        }

        public virtual bool Equals(BuildTask? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Name == Name;
        }
    }
}
