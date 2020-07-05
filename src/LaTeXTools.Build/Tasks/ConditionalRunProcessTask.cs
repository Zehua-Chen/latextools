using System;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class ConditionalRunProcessTask : RunProcessTask
    {
        public Func<ValueTask<bool>> Condition { get; set; } = () => new ValueTask<bool>(true);

        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (await this.Condition())
            {
                await base.RunAsync(logger);
            }
        }
    }
}
