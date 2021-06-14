using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks.Tests
{
    public class TestTask : BuildTask
    {
        public bool HasRun { get; set; }

        public override ValueTask RunAsync(BuildContext context)
        {
            HasRun = true;

            return base.RunAsync(context);
        }
    }
}
