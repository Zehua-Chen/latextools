using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.IO;
using System.IO.Abstractions;
using LaTeXTools.Project;
using LaTeXTools.Build;
using LaTeXTools.Build.Tasks;
using LaTeXTools.Build.Generators;

namespace latextools
{
    public class ExportHandler : ICommandHandler
    {
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var logger = new Logger();
            LaTeXProject? project = await LaTeXProject.FindAsync("latexproject.json");

            if (project == null)
            {
                logger.Message("no project found");
                return -1;
            }

            var build = new LaTeXBuild(project);
            var fileSystem = new FileSystem();

            string makePath = Path.Combine(project.WorkingDirectory, "Makefile");

            ProjectTask task = await build.GetBuildTaskAsync(fileSystem);

            using StreamWriter writer = File.CreateText("Makefile");
            await writer.WriteMakefileAsync(task.GetMakefile());

            return 0;
        }
    }
}
