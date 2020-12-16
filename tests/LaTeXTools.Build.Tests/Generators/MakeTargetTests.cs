using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LaTeXTools.Build.Generators;

namespace LaTeXTools.Build.Tests.Generators
{
    public class MakeTargetTests
    {
        private class Data : IEnumerable<object[]>
        {
            private object[] Regular()
            {
                var builder = new StringBuilder();
                builder.AppendLine("target: file_a file_b | o_file_a o_file_b");
                builder.AppendLine("\techo 1");
                builder.AppendLine("\techo 2");

                return new object[]
                {
                    new MakeTarget()
                    {
                        Name = "target",
                        IsPhony = false,
                        Commands = new List<string>()
                        {
                            "echo 1",
                            "echo 2"
                        },
                        Dependencies = new List<string>()
                        {
                            "file_a",
                            "file_b"
                        },
                        OrderOnlyDependencies = new List<string>()
                        {
                            "o_file_a",
                            "o_file_b",
                        }
                    },
                    builder.ToString()
                };
            }

            private object[] Phony()
            {
                var builder = new StringBuilder();
                builder.AppendLine(".PHONY: target");
                builder.AppendLine("target: file | o_file");
                builder.AppendLine("\techo 1");

                return new object[]
                {
                    new MakeTarget()
                    {
                        Name = "target",
                        IsPhony = true,
                        Commands = new List<string>()
                        {
                            "echo 1"
                        },
                        Dependencies = new List<string>()
                        {
                            "file"
                        },
                        OrderOnlyDependencies = new List<string>()
                        {
                            "o_file"
                        }
                    },
                    builder.ToString()
                };
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return Regular();
                yield return Phony();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(Data))]
        public async Task Write(MakeTarget target, string expected)
        {
            using var writer = new StringWriter();
            await target.WriteAsync(writer);

            string actual = writer.ToString();
            Assert.Equal(expected, actual);
        }
    }
}
