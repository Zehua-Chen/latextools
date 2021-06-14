using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Xunit;
using LaTeXTools.Build.Tests;

namespace LaTeXTools.Build.Tasks.Tests
{
    public sealed class RunIfFileContentsDifferTaskTests
    {
        [Theory]
        [InlineData("original", "new", true)]
        [InlineData("same", "same", true)]
        public async ValueTask Test(string originalData, string newData, bool run)
        {
            var testTask = new TestTask();
            var task = new RunIfFileContentsDifferTask()
            {
                Task = testTask,
                FileContentComparisons = new FileContentComparison[]
                {
                    new FileContentComparison("index.aux", originalData)
                }
            };

            var fs = new MockFileSystem();
            var context = new BuildContext(fs, new MemoryLogger());

            fs.AddFile("index.aux", new MockFileData(newData));

            await task.RunAsync(context);

            Assert.Equal(testTask.HasRun, run);
        }
    }
}
