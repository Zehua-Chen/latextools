using System;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class ConditionalGroupTask : GroupTask
    {
        public Func<ValueTask<bool>> Condition { get; set; } = () => new ValueTask<bool>(false);

        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (await this.Condition())
            {
                await base.RunAsync(logger);
            }
        }
    }
}
