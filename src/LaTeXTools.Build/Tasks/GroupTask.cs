using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class GroupTask : BuildTask
    {
        public bool Synchronous { get; set; } = true;
        public List<BuildTask> Children { get; set; } = new List<BuildTask>();

        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (this.Synchronous)
            {
                await this.RunSynchronouslyAsync(logger);
            }
            else
            {
                await this.RunAsynchronouslyAsync(logger);
            }
        }

        private ValueTask RunAsynchronouslyAsync(ILogger? logger)
        {
            throw new NotImplementedException();
        }

        private async ValueTask RunSynchronouslyAsync(ILogger? logger)
        {
            foreach (var task in this.Children)
            {
                await task.RunAsync(logger);
            }
        }
    }
}
