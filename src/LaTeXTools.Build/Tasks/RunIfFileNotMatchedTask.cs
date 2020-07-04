using System;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LaTeXTools.Build.Tasks
{
    public class RunIfFileNotMatchedTask : RunTask
    {
        public string Path { get; set; }
        public string Content { get; set; }

        public RunIfFileNotMatchedTask(
            ProcessStartInfo startInfo,
            string path,
            string content) : base(startInfo)
        {
            this.Path = path;
            this.Content = content;
        }

        public override async ValueTask RunAsync(TaskFactory taskFactory)
        {
            string newContent = await File.ReadAllTextAsync(this.Path);

            if (newContent != this.Content)
            {
                await base.RunAsync(taskFactory);
            }
        }
    }
}
