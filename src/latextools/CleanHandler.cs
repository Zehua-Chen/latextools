using System;
using System.Threading.Tasks;
using System.IO;
using System.CommandLine;
using System.CommandLine.Invocation;
using LaTeXTools.Project;

namespace LaTeXTools.CLI
{
    public class CleanHandler : ICommandHandler
    {
        public static Command Command
        {
            get
            {
                var command = new Command("clean", "Clean the build folder");
                command.Handler = new CleanHandler();

                return command;
            }
        }
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            string config = Path.Combine(Environment.CurrentDirectory, "latexproject.json");

            if (File.Exists(config))
            {
                LaTeXProject project = await LaTeXProject.LoadAsync(config);

                if (Directory.Exists(project.Bin))
                {
                    Directory.Delete(project.Bin, true);
                }
            }

            return 0;
        }
    }
}
