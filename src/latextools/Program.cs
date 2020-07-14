using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace latextools
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var @new = new Command("new", "Create a new project")
            {
                new Option<string>(
                    aliases: new string[] { "-n", "--name" },
                    description: "name of the project")
            };
            @new.Handler = new NewHandler();

            var build = new Command("build", "Build LaTeX target");
            build.Handler = new BuildHandler();

            var clean = new Command("clean", "Clean the build folder");
            clean.Handler = new CleanHandler();

            var generate = new Command("generate", "Generate a Makefile");
            generate.Handler = new GenerateHandler();

            RootCommand application = new RootCommand("LaTeX tools")
            {
                @new,
                build,
                clean,
                generate,
            };

            await application.InvokeAsync(args);
        }
    }
}
