using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public sealed class ProjectTask
    {
        public string OutputFilePath { get; set; } = "";
        public string OutputDirectory { get; set; } = "";
        public IEnumerable<string>? DependencyPaths { get; set; }
        public IEnumerable<BuildTask>? BuildTasks  { get; set; }
        public IEnumerable<ProjectTask>? SubProjects { get; set; }

        public async ValueTask RunAsync(ILogger? logger = null)
        {
            if (!Directory.Exists(this.OutputDirectory))
            {
                Directory.CreateDirectory(this.OutputDirectory);
            }

            if (!this.ShouldRun())
            {
                return;
            }

            await this.RunSubProjects(logger);

            if (this.BuildTasks == null)
            {
                return;
            }

            foreach (var buildTask in this.BuildTasks)
            {
                await buildTask.RunAsync(logger);
            }
        }

        private bool ShouldRun()
        {
            if (!File.Exists(this.OutputFilePath))
            {
                return true;
            }

            if (this.DependencyPaths == null)
            {
                return false;
            }

            var pdfWriteTime = (new FileInfo(this.OutputFilePath)).LastWriteTimeUtc;

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

        private async ValueTask RunSubProjects(ILogger? logger)
        {
            if (this.SubProjects == null)
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var subproject in this.SubProjects)
            {
                tasks.Add(subproject.RunAsync(logger).AsTask());
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
