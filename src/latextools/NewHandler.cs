using System;
using System.IO;
using System.Threading.Tasks;
using System.CommandLine.Parsing;
using System.CommandLine.Invocation;
using LaTeXTools.Project;

namespace latextools
{
    public class NewHandler : ICommandHandler
    {
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            ParseResult result = context.ParseResult;

            string? name = (string?)result.ValueForOption("--name");
            string workingDirectory = Environment.CurrentDirectory;

            if (name != null)
            {
                workingDirectory = Path.Combine(workingDirectory, name);

                if (!Directory.Exists(workingDirectory))
                {
                    Directory.CreateDirectory(workingDirectory);
                }

                Environment.CurrentDirectory = workingDirectory;
            }

            var project = new LaTeXProject();

            await project.WriteAsync(Path.Combine(workingDirectory, "latexproject.json"));
            await CreateEntryFileAsync(Path.Combine(workingDirectory, "index.tex"));

            return 0;
        }

        private async ValueTask CreateEntryFileAsync(string path)
        {
            using FileStream stream = File.OpenWrite(path);
            using var writer = new StreamWriter(stream);

            var content = @"\documentclass{article}

\begin{document}
  Hello World!
\end{document}";

            await writer.WriteAsync(content);
        }
    }
}
