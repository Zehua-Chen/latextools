using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO.Abstractions;
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

        public async ValueTask<ProjectTask> GetBuildTaskAsync(IFileSystem fileSystem)
        {
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

            buildTasks.Add(new RunIfFileContentsDifferTask()
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
                DependencyPaths = this.GetAllDependencies(fileSystem),
                BuildTasks = buildTasks
            };

            return task;
        }

        /// <summary>
        /// Traverse folders and get all dependencies of a project
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        private IEnumerable<string> GetAllDependencies(IFileSystem fileSystem)
        {
            var toVisit = new Queue<string>();
            IDirectory directory = fileSystem.Directory;

            foreach (var include in this.Root.GetShallowDependencies())
            {
                toVisit.Enqueue(include);
            }

            while (toVisit.Count != 0)
            {
                string item = toVisit.Dequeue();

                if (directory.Exists(item))
                {
                    foreach (var child in directory.EnumerateFileSystemEntries(item))
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
