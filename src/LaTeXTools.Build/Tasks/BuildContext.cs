using LaTeXTools.Build.IO;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public sealed class BuildContext
    {
        public IFileSystem FileSystem { get; init; }
        public ILogger Logger { get; init; }

        public BuildContext(IFileSystem fileSystem, ILogger logger)
        {
            FileSystem = fileSystem;
            Logger = logger;
        }
    }
}
