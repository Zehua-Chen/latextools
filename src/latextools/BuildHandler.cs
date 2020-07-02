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
            string config = Path.Combine(Environment.CurrentDirectory, "latexproject.json");
            LaTeXProject project = new LaTeXProject();

            if (File.Exists(config))
            {
                project = await LaTeXProject.LoadAsync(config);
            }

            LaTexBuild build = new LaTexBuild(project, TaskScheduler.Current);

            return await build.BuildAsync();
        }
    }
}
