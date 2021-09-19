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
        public void Bib()
        {
            var project = new LaTeXProject();

            project.Bib = "foo";
            Assert.Throws<ArgumentException>(() => project.ThrowIfBibNotSupported());

            project.Bib = "biber";
            project.ThrowIfBibNotSupported();

            project.Bib = "none";
            project.ThrowIfBibNotSupported();
        }

        [Fact]
        public void Dependencies()
        {
            var fs = new MockFileSystem();
            var project = new LaTeXProject()
            {
                Entry = "index.tex",
                Includes = new string[] { "topics/", "other.tex" },
            };

            // index.tex not found
            Assert.ThrowsAny<Exception>(() => project.ThrowIfDependenciesNotFound(fs));

            fs.AddFile("index.tex", new MockFileData(""));

            // other.tex not found
            Assert.ThrowsAny<Exception>(() => project.ThrowIfDependenciesNotFound(fs));

            fs.AddFile("other.tex", new MockFileData(""));

            // topics/ not found
            Assert.ThrowsAny<Exception>(() => project.ThrowIfDependenciesNotFound(fs));

            fs.AddDirectory("topics");

            project.ThrowIfDependenciesNotFound(fs);
        }
    }
}
