using System;
using System.Threading.Tasks;
using LaTeXTools.Build;

namespace LaTeXTools.Build.Tasks
{
    public class BuildTask : IEquatable<BuildTask>
    {
        public virtual ValueTask RunAsync(BuildContext context)
        {
            return new ValueTask();
        }

        public virtual bool Equals(BuildTask? other)
        {
            if (other == null)
            {
                return false;
            }

            return true;
        }
    }
}
