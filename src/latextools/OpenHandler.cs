using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.CommandLine;
using System.CommandLine.Invocation;
using LaTeXTools.Project;

namespace latextools
{
    public class OpenHandler : ICommandHandler
    {
        public static Command Command
        {
            get
            {
                var command = new Command("open", "Open the pdf file");
                command.Handler = new OpenHandler();

                return command;
            }
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var logger = new Logger();
            LaTeXProject? project = await LaTeXProject.FindAsync("latexproject.json");

            if (project == null)
            {
                logger.LogError("no project found");
                return -1;
            }

            if (!File.Exists(project.GetPDFPath()))
            {
                logger.LogError($"{project.GetPDFPath()} does not exists");
                return -1;
            }

            ProcessStartInfo startInfo = this.GetStartInfo(project.GetPDFPath());

            await Task.Run(() =>
            {
                logger.Log($"{startInfo.FileName} {startInfo.Arguments}");
                var process = System.Diagnostics.Process.Start(startInfo);

                if (process == null)
                {
                    throw new Exception($"Failed to start {startInfo.FileName}");
                }

                process.WaitForExit();
            });

            return 0;
        }

        ProcessStartInfo GetStartInfo(string pdf)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new ProcessStartInfo("open", $"{pdf}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new ProcessStartInfo($"{pdf}");
            }

            throw new NotImplementedException("Not implemented for this platform");
        }
    }
}
