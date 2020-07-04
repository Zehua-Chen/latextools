using System.IO;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    public class CreateDirectoryTask : BuildTask
    {
        public string Directory { get; set; }

        public CreateDirectoryTask(string directory)
        {
            this.Directory = directory;
        }

        public override ValueTask RunAsync()
        {
            if (!System.IO.Directory.Exists(this.Directory))
            {
                System.IO.Directory.CreateDirectory(this.Directory);
            }

            return new ValueTask();
        }
    }
}
