using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class CreateDirectoryTask : BuildTask
    {
        public string Directory { get; set; }

        public CreateDirectoryTask(string directory)
        {
            this.Directory = directory;
        }

        public override ValueTask RunAsync(ILogger? logger)
        {
            if (!System.IO.Directory.Exists(this.Directory))
            {
                logger?.Message($"create directory: {this.Directory}");
                System.IO.Directory.CreateDirectory(this.Directory);
            }

            return new ValueTask();
        }
    }
}
