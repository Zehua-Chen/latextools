using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using LaTeXTools.Project;
using LaTeXTools.Build.Tasks;

namespace LaTeXTools.Build
{
    public class LaTeXBuild
    {
        public LaTeXProject Root { get; private set; }

        public LaTeXBuild(LaTeXProject root)
        {
            this.Root = root;
        }

        public async ValueTask<ProjectTask> GetBuildTaskAsync()
        {
            this.Root.Validate();

            var buildTasks = new List<BuildTask>()
            {
                new RunProcessTask()
                {
                    StartInfo = this.Root.GetLaTeXStartInfo()
                }
            };

            if (Root.Bib != "none")
            {
                buildTasks.Add(new RunProcessTask()
                {
                    StartInfo = Root.GetBibStartInfo()
                });
            }

            if (Root.Glossary)
            {
                buildTasks.Add(new RunProcessTask()
                {
                    StartInfo = Root.GetGlossaryStartInfo()
                });
            }

            buildTasks.Add(new FileContentComparisonsTask()
            {
                Task = new RunProcessTask()
                {
                    StartInfo = this.Root.GetLaTeXStartInfo()
                },
                FileContentComparisons = await GetFileContentComparisonsAsync(),
            });

            var task = new ProjectTask()
            {
                OutputPDFPath = this.Root.GetPDFPath(),
                OutputDirectory = this.Root.Bin,
                DependencyPaths = this.GetIncludes(),
                BuildTasks = buildTasks
            };

            return task;
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

        private async ValueTask<IEnumerable<FileContentComparison?>> GetFileContentComparisonsAsync()
        {
            var comparison = new List<FileContentComparison>()
            {
                await FileContentComparison.OpenAsync(this.Root.GetAUXPath())
            };

            if (this.Root.Glossary)
            {
                comparison.Add(await FileContentComparison.OpenAsync(this.Root.GetGLSPath()));
                comparison.Add(await FileContentComparison.OpenAsync(this.Root.GetGLSDefsPath()));
            }

            return comparison.ToArray();
        }
    }
}
