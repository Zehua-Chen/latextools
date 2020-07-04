using System.Threading.Tasks;
using System.Diagnostics;

namespace LaTeXTools.Build.Tasks
{
    public class RunTask : BuildTask
    {
        public ProcessStartInfo StartInfo { get; set; }

        public RunTask(ProcessStartInfo startInfo)
        {
            this.StartInfo = startInfo;
        }

        public override async ValueTask RunAsync(TaskFactory taskFactory)
        {
            await taskFactory.StartNew(() =>
            {
                Process.Start(this.StartInfo).WaitForExit();
            });
        }
    }
}
