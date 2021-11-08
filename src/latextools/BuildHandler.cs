using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Invocation;
using LaTeXTools.Project;
using LaTeXTools.Build;
using LaTeXTools.Build.Tasks;

namespace LaTeXTools.CLI
{
    public class BuildHandler : ICommandHandler
    {
        public static Command Command
        {
            get
            {
                var command = new Command("build", "Build LaTeX target");
                command.Handler = new BuildHandler();

                return command;
            }
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var logger = new Logger();
            LaTeXProject? project = await LaTeXProject.FindAsync("latexproject.json");

            if (project == null)
            {
                await logger.LogErrorAsync("no project found");
                return -1;
            }

            if (Environment.CurrentDirectory != project.WorkingDirectory)
            {
                Environment.CurrentDirectory = project.WorkingDirectory;
            }

            var fileSystem = new FileSystem();
            var build = new LaTeXBuild(project);

            if (!build.CanBuild(fileSystem, out string message))
            {
                await logger.LogErrorAsync(message);
                return -1;
            }

            ProjectTask task = await build.GetBuildTaskAsync(fileSystem);
            var buildContext = new BuildContext(fileSystem, logger);

            try
            {
                await logger.LogAsync($"working directory: {Environment.CurrentDirectory}");
                await task.RunAsync(buildContext);
            }
            catch (AbortException abortException)
            {
                await logger.LogErrorAsync($"process exited with code {abortException.ExitCode}");
                await logger.LogFileAsync(File.ReadAllText(project.GetLogPath()));
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
