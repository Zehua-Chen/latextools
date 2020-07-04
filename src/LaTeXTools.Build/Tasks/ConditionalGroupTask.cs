using System;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    public class ConditionalGroupTask : GroupTask
    {
        public Func<bool> Condition { get; set; } = () => true;

        public override async ValueTask RunAsync(TaskFactory taskFactory)
        {
            if (!this.Condition())
            {
                return;
            }

            await base.RunAsync(taskFactory);
        }
    }
}
