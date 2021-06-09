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

        public override ValueTask RunAsync(BuildContext context)
        {
            ILogger logger = context.Logger;

            if (!System.IO.Directory.Exists(this.Directory))
            {
                logger.Log($"create directory: {this.Directory}");
                context.FileSystem.Directory.CreateDirectory(this.Directory);
            }

            return new ValueTask();
        }
    }
}
