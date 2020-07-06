using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class RunProcessTask : BuildTask
    {
        /// <summary>
        /// Note that this property would may be modified
        /// </summary>
        /// <value></value>
        public ProcessStartInfo? StartInfo { get; set; } = null;

        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (this.StartInfo == null)
            {
                return;
            }

            await Task.Run(async () =>
            {
                string action = $"{this.StartInfo.FileName} {this.StartInfo.Arguments}";

                logger?.LogAction(action);

                this.StartInfo.RedirectStandardError = true;
                this.StartInfo.RedirectStandardOutput = true;

                var process = Process.Start(this.StartInfo);
                process.WaitForExit();

                logger?.LogStdOut(action, await process.StandardOutput.ReadToEndAsync());
                logger?.LogStdErr(action, await process.StandardError.ReadToEndAsync());
            });
        }
    }
}
