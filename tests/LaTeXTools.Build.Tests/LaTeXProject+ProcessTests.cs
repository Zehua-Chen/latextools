using System.Diagnostics;
using Xunit;
using LaTeXTools.Project;

namespace LaTeXTools.Build.Tests.Generators
{
    public class LaTeXProjectProcessExtensionsTests
    {
        [Fact]
        public void LaTeXStartInfo()
        {
            var project = new LaTeXProject()
            {
                LaTeX = "pdflatex",
                Entry = "index.tex",
                Bin="bin"
            };

            ProcessStartInfo startInfo = project.GetLaTeXStartInfo();

            Assert.Equal("pdflatex", startInfo.FileName);
            Assert.Contains("-output-directory=bin", startInfo.Arguments);
            Assert.Contains("-interaction=batchmode", startInfo.Arguments);
            Assert.Contains("index.tex", startInfo.Arguments);
        }
    }
}
