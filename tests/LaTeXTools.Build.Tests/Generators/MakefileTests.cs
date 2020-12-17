using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LaTeXTools.Build.Generators;

namespace LaTeXTools.Build.Tests.Generators
{
    public class MakefileTests
    {
        [Fact]
        public async Task Write()
        {
            using var writer = new StringWriter();
            var makefile = new Makefile()
            {
                Targets = new List<MakeTarget>()
                {
                    new MakeTarget()
                    {
                        Name = "target1",
                        Commands = new List<string>()
                        {
                            "command"
                        }
                    },
                    new MakeTarget()
                    {
                        Name = "target2",
                        Commands = new List<string>()
                        {
                            "command"
                        }
                    }
                }
            };

            await writer.WriteMakefileAsync(makefile);

            var expected = new StringBuilder();
            expected.AppendLine($"# {makefile.TopLevelComment}");
            expected.AppendLine();

            expected.AppendLine("target1:");
            expected.AppendLine("\tcommand");
            expected.AppendLine();

            expected.AppendLine("target2:");
            expected.AppendLine("\tcommand");
            expected.AppendLine();

            Assert.Equal(expected.ToString(), writer.ToString());
        }
    }
}
