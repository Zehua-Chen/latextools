using System;
using Xunit;
using LaTeXTools.Project;

namespace LaTeXTools.Project.Tests
{
    public class LaTeXProjectPathsExtensionsTests
    {
        [Fact]
        public void Log()
        {
            var project = new LaTeXProject()
            {
                Bin = "bin",
                Entry = "index.tex"
            };

            Assert.Equal("bin/index.log", project.GetLogPath());
        }
    }
}
