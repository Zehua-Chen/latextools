using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using LaTeXTools.Project;

namespace LaTeXTools.Build
{
    public class LaTexBuild
    {
        TaskFactory _taskFactory = null;

        public LaTeXProject Root { get; private set; }
        public TaskScheduler Scheduler { get; private set; }

        public LaTexBuild(LaTeXProject root, TaskScheduler scheduler)
        {
            this.Root = root;
            this.Scheduler = scheduler;
            _taskFactory = new TaskFactory(scheduler);
        }

        public async ValueTask<int> BuildAsync()
        {
            if (!this.ShouldBuild())
            {
                return 0;
            }

            string oldAUX = await File.ReadAllTextAsync(this.Root.GetAUXPath());

            await this.Build();

            string newAUX = await File.ReadAllTextAsync(this.Root.GetAUXPath());

            if (oldAUX != newAUX)
            {
                await this.Build();
            }

            return 0;
        }

        private bool ShouldBuild()
        {
            if (!Directory.Exists(this.Root.Bin))
            {
                Directory.CreateDirectory(this.Root.Bin);
                return true;
            }

            if (!File.Exists(this.Root.GetPDFPath()))
            {
                return true;
            }

            Queue<string> dependencies = new Queue<string>(this.Root.GetDependencyPaths());
            DateTime pdfWriteTime = (new FileInfo(this.Root.GetPDFPath())).LastWriteTimeUtc;

            while (dependencies.Count != 0)
            {
                var dependency = dependencies.Dequeue();

                if (Directory.Exists(dependency))
                {
                    foreach (var item in Directory.EnumerateFileSystemEntries(dependency))
                    {
                        dependencies.Enqueue(item);
                    }

                    continue;
                }

                var info = new FileInfo(dependency);

                if (info.LastWriteTimeUtc > pdfWriteTime)
                {
                    return true;
                }
            }

            return false;
        }

        private ValueTask Build()
        {
            return new ValueTask(_taskFactory.StartNew(() =>
            {
                var process = Process.Start(this.Root.GetStartInfo());
                process.WaitForExit();
            }));
        }
    }
}
