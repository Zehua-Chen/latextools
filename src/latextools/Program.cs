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

            var export = new Command("export", "Export a Makefile");
            export.Handler = new ExportHandler();

            var open = new Command("open", "Open the pdf file");
            open.Handler = new OpenHandler();

            RootCommand application = new RootCommand("LaTeX tools")
            {
                @new,
                build,
                clean,
                export,
                open
            };

            await application.InvokeAsync(args);
        }
    }
}
