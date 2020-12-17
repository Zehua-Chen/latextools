using System;
using System.IO;
using System.Threading.Tasks;
using System.CommandLine.Invocation;
using LaTeXTools.Project;
using LaTeXTools.Build;
using LaTeXTools.Build.Tasks;

namespace latextools
{
    public class BuildHandler : ICommandHandler
    {
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var logger = new Logger();
            LaTeXProject? project = await LaTeXProject.FindAsync("latexproject.json");

            if (project == null)
            {
                logger.Error("no project found");
                return -1;
            }

            if (Environment.CurrentDirectory != project.WorkingDirectory)
            {
                Environment.CurrentDirectory = project.WorkingDirectory;
            }

            var build = new LaTeXBuild(project);
            ProjectTask task = await build.GetBuildTaskAsync();

            try
            {
                logger.Message($"working directory: {Environment.CurrentDirectory}");
                await task.RunAsync(logger);
            }
            catch (AbortException abortException)
            {
                logger.Error($"process exited with code {abortException.ExitCode}");
                logger.Error(File.ReadAllText(project.GetLogPath()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            return 0;
        }
    }
}
