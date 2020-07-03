using System;
using System.Threading.Tasks;
using System.IO;
using System.CommandLine.Invocation;
using LaTeXTools.Project;

namespace latextools
{
    public class CleanHandler : ICommandHandler
    {
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
