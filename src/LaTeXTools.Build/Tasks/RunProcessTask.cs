using System.Threading.Tasks;
using System.Diagnostics;

namespace LaTeXTools.Build.Tasks
{
    public class RunProcessTask : BuildTask
    {
        public ProcessStartInfo StartInfo { get; set; }

        public RunProcessTask(ProcessStartInfo startInfo)
        {
            this.StartInfo = startInfo;
        }

        public override async ValueTask RunAsync()
        {
            await Task.Run(() =>
            {
                Process.Start(this.StartInfo).WaitForExit();
            });
        }
    }
}
