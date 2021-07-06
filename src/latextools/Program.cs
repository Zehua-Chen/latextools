using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace LaTeXTools.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(string.Join(", ", args));

            var newCommand = NewHandler.Command;
            newCommand.Handler = new NewHandler();

            RootCommand application = new RootCommand("LaTeX Tools")
            {
                newCommand,
                BuildHandler.Command,
                CleanHandler.Command,
                ExportHandler.Command,
                OpenHandler.Command
            };

            await application.InvokeAsync(args);
        }
    }
}
