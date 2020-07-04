using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using LaTeXTools.Project;
using LaTeXTools.Build.Tasks;

namespace LaTeXTools.Build
{
    public class LaTexBuild
    {
        TaskFactory _taskFactory;

        public LaTeXProject Root { get; private set; }
        public TaskScheduler Scheduler { get; private set; }

        public LaTexBuild(LaTeXProject root, TaskScheduler scheduler)
        {
            this.Root = root;
            this.Scheduler = scheduler;
            _taskFactory = new TaskFactory(scheduler);
        }

        public async ValueTask<BuildTask> GetBuildTaskAsync()
        {
            string oldAUX = await File.ReadAllTextAsync(this.Root.GetAUXPath());

            var mainTask = new RunIfFileOutdatedGroupTask()
            {
                FilePath = this.Root.GetPDFPath(),
                DependencyPaths = this.GetIncludes(),
                Children = new List<BuildTask>()
                {
                    new RunProcessTask(this.Root.GetLaTeXStartInfo()),
                }
            };

            if (this.Root.Bib != "none")
            {
                mainTask.Children.Add(new RunProcessTask(this.Root.GetBibStartInfo()));
            }

            var groupTask = new GroupTask()
            {
                Children = new List<BuildTask>()
                {
                    new CreateDirectoryTask(this.Root.Bin),
                    mainTask,
                    new ConditionalGroupTask()
                    {
                        Condition = async () =>
                        {
                            string newAux = await File.ReadAllTextAsync(this.Root.GetAUXPath());

                            return newAux != oldAUX;
                        },
                        Children = new List<BuildTask>()
                        {
                            new RunProcessTask(this.Root.GetLaTeXStartInfo())
                        }
                    }
                }
            };

            return groupTask;
        }

        private IEnumerable<string> GetIncludes()
        {
            var toVisit = new Queue<string>();


            foreach (var include in this.Root.GetDependencyPaths())
            {
                toVisit.Enqueue(include);
            }

            while (toVisit.Count != 0)
            {
                string item = toVisit.Dequeue();

                if (Directory.Exists(item))
                {
                    foreach (var child in Directory.EnumerateFileSystemEntries(item))
                    {
                        toVisit.Enqueue(child);
                    }

                    continue;
                }

                yield return item;
            }
        }
    }
}
