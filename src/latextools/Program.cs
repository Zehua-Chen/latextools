using System.CommandLine;
using System.Threading.Tasks;

namespace LaTeXTools.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RootCommand application = new RootCommand("LaTeX Tools")
            {
                NewHandler.Command,
                BuildHandler.Command,
                CleanHandler.Command,
                ExportHandler.Command,
                OpenHandler.Command
            };

            await application.InvokeAsync(args);
        }
    }
}
