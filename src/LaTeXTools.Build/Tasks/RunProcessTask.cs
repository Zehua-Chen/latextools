using System;
using System.Threading.Tasks;
using System.Diagnostics;
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
                string action = $"{StartInfo.FileName} {StartInfo.Arguments}";

                logger?.Message(action);

                StartInfo.RedirectStandardError = true;
                StartInfo.RedirectStandardOutput = true;

                var process = Process.Start(StartInfo);

                if (process == null)
                {
                    throw new Exception($"Faield to start proeces {StartInfo.FileName}");
                }

                process.WaitForExit();

                logger?.StdOut(action, await process.StandardOutput.ReadToEndAsync());
                logger?.StdErr(action, await process.StandardError.ReadToEndAsync());

                if (process.ExitCode != 0)
                {
                    throw new AbortException()
                    {
                        ExitCode = process.ExitCode
                    };
                }
            });
        }
    }
}
