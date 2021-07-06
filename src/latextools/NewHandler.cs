using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Invocation;
using LaTeXTools.Project;

namespace LaTeXTools.CLI
{
    /// <summary>
    /// <c>latextools new</c>; create a new LaTeX project
    ///
    /// Passing <c>--name</c> parameter will cause this command to create the project in a new
    /// folder; not passing it will cause this command to create the project in place
    /// </summary>
    public class NewHandler : ICommandHandler
    {
        public static Command Command
        {
            get
            {
                var command = new Command("new", "Create a new project")
                {
                    new Option<string>(
                        aliases: new string[] { "-n", "--name" },
                        description: "name of the project")
                };

                return command;
            }
        }

        private IFileSystem _fileSystem;
        private string _currentDirectory;

        public NewHandler() : this(new FileSystem(), Environment.CurrentDirectory)
        {
        }

        public NewHandler(IFileSystem fileSystem, string currentDirectory)
        {
            _fileSystem = fileSystem;
            _currentDirectory = currentDirectory;
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            ParseResult result = context.ParseResult;

            string? name = (string?)result.ValueForOption("--name");
            string workingDirectory = _currentDirectory;

            if (name != null)
            {
                workingDirectory = Path.Combine(workingDirectory, name);

                if (!_fileSystem.Directory.Exists(workingDirectory))
                {
                    _fileSystem.Directory.CreateDirectory(workingDirectory);
                }

                // Environment.CurrentDirectory = workingDirectory;
            }

            var project = new LaTeXProject();

            await project.WriteAsync(
                Path.Combine(workingDirectory, "latexproject.json"),
                _fileSystem.File);

            await CreateEntryFileAsync(Path.Combine(workingDirectory, "index.tex"));

            return 0;
        }

        private async ValueTask CreateEntryFileAsync(string path)
        {
            using Stream stream = _fileSystem.File.OpenWrite(path);
            using var writer = new StreamWriter(stream);

            var content = @"\documentclass{article}

\begin{document}
  Hello World!
\end{document}";

            await writer.WriteAsync(content);
        }
    }
}
