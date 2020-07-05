using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class RunIfFileOutdatedGroupTask : GroupTask
    {
        public string FilePath { get; set; } = "";
        public IEnumerable<string>? DependencyPaths { get; set; }

        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (!this.ShouldRun())
            {
                return;
            }

            await base.RunAsync(logger);
        }

        private bool ShouldRun()
        {
            if (!File.Exists(this.FilePath))
            {
                return true;
            }

            if (this.DependencyPaths == null)
            {
                return false;
            }

            var pdfWriteTime = (new FileInfo(this.FilePath)).LastWriteTimeUtc;

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
    }
}
