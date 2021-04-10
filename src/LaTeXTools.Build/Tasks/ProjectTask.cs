using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

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
            if (!Directory.Exists(this.OutputDirectory))
            {
                Directory.CreateDirectory(this.OutputDirectory);
            }

            if (!this.ShouldRun())
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

        private bool ShouldRun()
        {
            if (!File.Exists(this.OutputPDFPath))
            {
                return true;
            }

            if (this.DependencyPaths == null)
            {
                return false;
            }

            var pdfWriteTime = (new FileInfo(this.OutputPDFPath)).LastWriteTimeUtc;

            foreach (var file in this.DependencyPaths)
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
