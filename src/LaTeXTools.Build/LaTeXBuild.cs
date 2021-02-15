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

            string oldAUX = "";
            string oldGLS = "";

            if (File.Exists(this.Root.GetAUXPath()))
            {
                oldAUX = await File.ReadAllTextAsync(Root.GetAUXPath());
            }

            if (Root.Glossary && File.Exists(Root.GetGLSPath()))
            {
                oldGLS = await File.ReadAllTextAsync(Root.GetGLSPath());
            }

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

            buildTasks.Add(new ConditionalRunProcessTask()
            {
                Condition = async () =>
                {
                    string newAUX = await File.ReadAllTextAsync(this.Root.GetAUXPath());

                    if (!Root.Glossary)
                    {
                        return newAUX != oldAUX;
                    }

                    string newGLS = await File.ReadAllTextAsync(this.Root.GetGLSPath());

                    return newAUX != oldAUX || oldGLS != newGLS;
                },
                StartInfo = this.Root.GetLaTeXStartInfo()
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
    }
}
