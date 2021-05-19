using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace LaTeXTools.Build.Tasks
{
    public sealed class ProjectTask
    {
        public string OutputPDFPath { get; set; } = "";
        public string OutputDirectory { get; set; } = "";
        public IEnumerable<string>? DependencyPaths { get; set; }
        public IEnumerable<BuildTask>? BuildTasks { get; set; }
        public IEnumerable<ProjectTask>? SubProjects { get; set; }

        /// <summary>
        /// Run the task
        /// </summary>
        /// <param name="logger">an optional logger</param>
        /// <exception cref="LaTeXTools.Build.AbortException">
        /// Thrown when the build needs to be aborted
        /// </exception>
        /// <returns></returns>
        public async ValueTask RunAsync(BuildContext context)
        {
            IFileSystem fileSystem = context.FileSystem;
            IDirectory directory = fileSystem.Directory;

            if (!directory.Exists(this.OutputDirectory))
            {
                directory.CreateDirectory(this.OutputDirectory);
            }

            if (!this.ShouldRun(fileSystem))
            {
                context.Logger.Message("no build needed");
                return;
            }

            await this.RunSubProjects(context);

            if (this.BuildTasks == null)
            {
                return;
            }

            foreach (var buildTask in this.BuildTasks)
            {
                await buildTask.RunAsync(context);
            }
        }

        private bool ShouldRun(IFileSystem fileSystem)
        {
            IFile file = fileSystem.File;

            if (!file.Exists(this.OutputPDFPath))
            {
                return true;
            }

            if (this.DependencyPaths == null)
            {
                return false;
            }

            DateTime pdfWriteTime = file.GetLastWriteTimeUtc(this.OutputPDFPath);

            foreach (var dependency in this.DependencyPaths)
            {
                if (file.Exists(dependency))
                {
                    if (file.GetLastWriteTimeUtc(dependency) > pdfWriteTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private async ValueTask RunSubProjects(BuildContext context)
        {
            if (this.SubProjects == null)
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var subproject in this.SubProjects)
            {
                tasks.Add(subproject.RunAsync(context).AsTask());
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
