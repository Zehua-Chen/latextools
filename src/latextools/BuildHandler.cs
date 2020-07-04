using System;
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
            LaTeXProject project = await LaTeXProject.FindAsync(
                "latexproject.json",
                new LaTeXProject());

            if (Environment.CurrentDirectory != project.WorkingDirectory)
            {
                Environment.CurrentDirectory = project.WorkingDirectory;
            }

            var build = new LaTexBuild(project, TaskScheduler.Current);
            BuildTask task = await build.GetBuildTaskAsync();

            try
            {
                await task.RunAsync();
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
