using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    public class GroupTask : BuildTask
    {
        public bool Synchronous { get; set; } = true;
        public List<BuildTask> Children { get; set; } = new List<BuildTask>();

        public override async ValueTask RunAsync(TaskFactory taskFactory)
        {
            if (this.Synchronous)
            {
                await this.RunSynchronouslyAsync(taskFactory);
            }
            else
            {
                await this.RunAsynchronouslyAsync(taskFactory);
            }
        }

        private ValueTask RunAsynchronouslyAsync(TaskFactory factory)
        {
            throw new NotImplementedException();
        }

        private async ValueTask RunSynchronouslyAsync(TaskFactory factory)
        {
            foreach (var task in this.Children)
            {
                await task.RunAsync(factory);
            }
        }
    }
}
