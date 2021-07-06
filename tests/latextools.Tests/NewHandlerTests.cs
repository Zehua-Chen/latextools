using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace LaTeXTools.CLI.Tests
{
    public class NewHandlerTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[]
                {
                    new string[] { "new" },
                    new string[] { "latexproject.json", "index.tex" }
                };

                yield return new object[]
                {
                    new string[] { "new", "--name", "foo" },
                    new string[]
                    {
                        Path.Combine("foo", "latexproject.json"),
                        Path.Combine("foo", "index.tex")
                    }
                };
            }
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Test(string[] args, string[] createdFiles)
        {
            var fs = new MockFileSystem();
            var newCommand = NewHandler.Command;
            newCommand.Handler = new NewHandler(fs, Path.VolumeSeparatorChar.ToString());

            var root = new RootCommand()
            {
                newCommand
            };

            Assert.Equal(0, root.Invoke(args));

            foreach (string createdFile in createdFiles)
            {
                Assert.True(fs.FileExists(createdFile));
            }
        }
    }
}
