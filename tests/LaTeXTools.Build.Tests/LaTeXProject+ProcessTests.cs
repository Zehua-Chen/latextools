using System.Diagnostics;
using System.Collections.Generic;
using Xunit;
using LaTeXTools.Project;

namespace LaTeXTools.Build.Tests
{
    public class LaTeXProjectProcessExtensionsTests
    {
        [Fact]
        public void BareboneProject()
        {
            var project = new LaTeXProject()
            {
                LaTeX = "pdflatex",
                Entry = "index.tex",
                Bin = "bin",
            };

            ProcessStartInfo startInfo = project.GetLaTeXStartInfo();

            Assert.Equal("pdflatex", startInfo.FileName);
            Assert.Contains("-output-directory=bin", startInfo.Arguments);
            Assert.Contains("-interaction=batchmode", startInfo.Arguments);
            Assert.Contains("index.tex", startInfo.Arguments);
        }

        [Fact]
        public void GlossaryProject()
        {
            var project = new LaTeXProject()
            {
                LaTeX = "pdflatex",
                Entry = "index.tex",
                Bin = "bin",
                Glossary = true,
            };

            ProcessStartInfo startInfo = project.GetGlossaryStartInfo();

            Assert.Equal("makeglossaries", startInfo.FileName);
            Assert.Contains("-d bin", startInfo.Arguments);
            Assert.Contains("index", startInfo.Arguments);
        }
    }
}
