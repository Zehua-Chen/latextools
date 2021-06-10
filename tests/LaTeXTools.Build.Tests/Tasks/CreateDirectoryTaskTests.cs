using System.Threading.Tasks;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using LaTeXTools.Build.Tests;

namespace LaTeXTools.Build.Tasks.Tests
{
    public class CreateDirectoryTaskTests
    {
        [Fact]
        public async ValueTask Test()
        {
            var task = new CreateDirectoryTask("bin");
            var fs = new MockFileSystem();
            var logger = new MemoryLogger();

            var context = new BuildContext(fs, logger);
            await task.RunAsync(context);

            Assert.True(fs.Directory.Exists("bin"));
        }
    }
}
