using System;
using System.IO;
using System.Threading.Tasks;
using System.CommandLine.Invocation;
using LaTeXTools.Project;
using LaTeXTools.Build;

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

            LaTexBuild build = new LaTexBuild(project, TaskScheduler.Current);

            return await build.BuildAsync();
        }
    }
}
