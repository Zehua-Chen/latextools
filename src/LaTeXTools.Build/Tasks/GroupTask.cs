using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    public class GroupTask : BuildTask
    {
        public bool Synchronous { get; set; } = true;
        public List<BuildTask> Children { get; set; } = new List<BuildTask>();

        public override async ValueTask RunAsync()
        {
            if (this.Synchronous)
            {
                await this.RunSynchronouslyAsync();
            }
            else
            {
                await this.RunAsynchronouslyAsync();
            }
        }

        private ValueTask RunAsynchronouslyAsync()
        {
            throw new NotImplementedException();
        }

        private async ValueTask RunSynchronouslyAsync()
        {
            foreach (var task in this.Children)
            {
                await task.RunAsync();
            }
        }
    }
}
