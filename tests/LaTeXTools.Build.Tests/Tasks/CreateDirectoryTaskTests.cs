using System.Threading.Tasks;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using LaTeXTools.Build.Tests;

namespace LaTeXTools.Build.Tasks.Tests
{
    public sealed class CreateDirectoryTaskTests
    {
        [Fact]
        public async ValueTask DirectoryExists()
        {
            var task = new CreateDirectoryTask("bin");
            var fs = new MockFileSystem();
            var logger = new MemoryLogger();

            var context = new BuildContext(fs, logger);

            Assert.True(fs.Directory.Exists("bin"));
            await task.RunAsync(context);
            Assert.True(fs.Directory.Exists("bin"));
        }

        [Fact]
        public async ValueTask DirectoryDoesNotExists()
        {
            var task = new CreateDirectoryTask("bin");
            var fs = new MockFileSystem();
            var logger = new MemoryLogger();

            var context = new BuildContext(fs, logger);

            Assert.False(fs.Directory.Exists("bin"));
            await task.RunAsync(context);
            Assert.True(fs.Directory.Exists("bin"));
        }
    }
}
