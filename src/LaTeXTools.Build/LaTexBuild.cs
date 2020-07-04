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

            var mainTask = new ConditionalGroupTask()
            {
                Condition = () =>
                {
                    if (!File.Exists(this.Root.GetPDFPath()))
                    {
                        return true;
                    }

                    var pdfWriteTime = (new FileInfo(this.Root.GetPDFPath())).LastWriteTimeUtc;

                    foreach (var file in this.GetIncludes())
                    {
                        if (File.Exists(file))
                        {
                            var info = new FileInfo(file);

                            if (info.LastWriteTimeUtc > pdfWriteTime)
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                },
                Children = new List<BuildTask>()
                {
                    new RunTask(this.Root.GetLaTeXStartInfo()),
                }
            };

            if (this.Root.Bib != "none")
            {
                mainTask.Children.Add(new RunTask(this.Root.GetBibStartInfo()));
            }

            mainTask.Children.Add(
                new RunIfFileNotMatchedTask(
                    this.Root.GetLaTeXStartInfo(),
                    this.Root.GetAUXPath(),
                    oldAUX));

            var groupTask = new GroupTask()
            {
                Children = new List<BuildTask>()
                {
                    new CreateDirectoryTask(this.Root.Bin),
                    mainTask
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
