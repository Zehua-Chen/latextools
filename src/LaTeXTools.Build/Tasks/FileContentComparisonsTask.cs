using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tasks
{
    public class FileContentComparisonsTask : BuildTask
    {
        /// <summary>
        /// The task ot execute if there is a file content comparison returns false.
        /// </summary>
        /// <remarks>
        /// An instance of <c>BuildTask</c> by default
        /// </remarks>
        /// <value></value>
        public BuildTask Task { get; set; }

        /// <summary>
        /// A list of file comparisons
        /// </summary>
        ///
        /// <remarks>
        /// <list>
        ///     <item>If an item is null then it is not executed</item>
        ///     <item>By default an empty array</item>
        /// </list>
        /// </remarks>
        /// <value></value>
        public IEnumerable<FileContentComparison?> FileContentComparisons { get; set; }

        public FileContentComparisonsTask() : base()
        {
            Task = new BuildTask();
            FileContentComparisons = new FileContentComparison[0];
        }

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
                    await Task.RunAsync(logger);

                    break;
                }
            }
        }

        public override bool Equals(BuildTask? other)
        {
            if (other == null || !(other is FileContentComparisonsTask))
            {
                return false;
            }

            var otherComparisonTask = (FileContentComparisonsTask)other;

            return base.Equals(other)
                && this.Task.Equals(otherComparisonTask.Task)
                && FileContentComparisons.SequenceEqual(otherComparisonTask.FileContentComparisons);
        }
    }
}