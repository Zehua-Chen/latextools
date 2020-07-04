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

            var scheduler = TaskScheduler.Current;
            var factory = new TaskFactory(scheduler);

            await task.RunAsync(factory);

            return 0;
        }
    }
}
