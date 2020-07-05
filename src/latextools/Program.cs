using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace latextools
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var build = new Command("build")
            {
                Description = "Build LaTeX target"
            };
            build.Handler = new BuildHandler();

            var clean = new Command("clean")
            {
                Description = "Clean the build folder"
            };
            clean.Handler = new CleanHandler();

            var generate = new Command("generate")
            {
                Description = "Generate a Makefile"
            };
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
