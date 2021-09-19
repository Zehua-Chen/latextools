using System;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using LaTeXTools.Project;

namespace LaTeXTools.Build.Tests
{
    public sealed class LaTeXProjectValidateExtensionsTests
    {
        [Fact]
        public void PathsExist()
        {
            var fs = new MockFileSystem();
            var project = new LaTeXProject()
            {
                Entry = "index.tex",
                Includes = new string[] { "topics/", "other.tex" },
            };

            fs.AddFile("index.tex", new MockFileData(""));

            // other.tex not found
            Assert.ThrowsAny<Exception>(() => project.PathsExist(fs));

            fs.AddFile("other.tex", new MockFileData(""));

            // topics/ not found
            Assert.ThrowsAny<Exception>(() => project.PathsExist(fs));

            fs.AddDirectory("topics");

            project.PathsExist(fs);
        }
    }
}
