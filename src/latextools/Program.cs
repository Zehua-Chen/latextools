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
            var build = new Command("build");
            build.Handler = new BuildHandler();

            var clean = new Command("clean");
            clean.Handler = new CleanHandler();

            var generate = new Command("generate");
            generate.Handler = new GenerateHandler();

            RootCommand application = new RootCommand("LaTeX tools")
            {
                build,
                clean,
                generate,
            };

            await application.InvokeAsync(args);
        }
    }
}
