using System;
using System.CommandLine;
using System.Threading.Tasks;
using LaTeXTools.Project;

namespace latextools
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RootCommand application = new RootCommand("LaTeX tools")
            {
                new Command("build")
                {
                    Handler = new BuildHandler()
                },
                new Command("clean")
                {
                    Handler = new CleanHandler()
                }
            };

            await application.InvokeAsync(args);
        }
    }
}
