using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.IO;
using System.Text;
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
                logger.LogMessage("no project found");
                return -1;
            }

            var build = new LaTexBuild(project);
            string makePath = Path.Combine(project.WorkingDirectory, "Makefile");

            ProjectTask task = await build.GetBuildTaskAsync();

            await task.GetMakefile().Write(makePath);

            return 0;
        }
    }
}
