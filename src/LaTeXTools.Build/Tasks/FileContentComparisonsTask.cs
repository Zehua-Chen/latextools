using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class FileContentComparisonsTask : BuildTask
    {
        public BuildTask? Task { get; set; }
        public IEnumerable<FileContentComparison?>? FileContentComparisons { get; set; }

        /// <summary>
        /// Execute <c>Task</c> if any file content comparisons returns false
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public override async ValueTask RunAsync(ILogger? logger)
        {
            if (FileContentComparisons == null)
            {
                return;
            }

            Task<bool>[] tasks = FileContentComparisons
                .Where(comparison => comparison != null)
                .Select((comparison, index) => { return comparison!.CompareAsync(); })
                .Select((task, index) => task.AsTask())
                .ToArray();

            bool[] comparisons = await Task<bool>.WhenAll(tasks);

            foreach (bool comparison in comparisons)
            {
                // there is a content mismatch
                if (!comparison)
                {
                    if (Task != null)
                    {
                        await Task.RunAsync(logger);
                    }

                    break;
                }
            }
        }
    }
}
