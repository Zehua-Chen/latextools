using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using LaTeXTools.Project;
using LaTeXTools.Build.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build
{
    public class LaTexBuild
    {
        public LaTeXProject Root { get; private set; }

        public LaTexBuild(LaTeXProject root)
        {
            this.Root = root;
        }

        public async ValueTask<BuildTask> GetBuildTaskAsync()
        {
            this.Root.Validate();

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
