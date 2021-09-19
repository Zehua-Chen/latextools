using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System;
using Xunit;
using LaTeXTools.Build.Tasks;
using LaTeXTools.Build.Generators;

namespace LaTeXTools.Build.Tests.Generators
{
    public class ProjectTaskMakeExtensionsTests
    {
        private class Data : IEnumerable<object[]>
        {
            public object[] SingleProject()
            {
                return new object[]
                {
                    new ProjectTask()
                    {
                        OutputPDFPath = "bin/index.pdf",
                        OutputDirectory = "bin",
                        DependencyPaths = new List<string>()
                        {
                            "index.tex"
                        },
                        BuildTasks = new List<BuildTask>()
                        {
                            new RunProcessTask()
                            {
                                StartInfo = new ProcessStartInfo()
                                {
                                    FileName = "pdflatex",
                                    Arguments = "-output-directory=bin -halt-on-error index.tex"
                                }
                            },
                            new RunIfFileContentsDifferTask()
                            {
                                Task = new RunProcessTask()
                                {
                                    StartInfo = new ProcessStartInfo()
                                    {
                                        FileName = "pdflatex",
                                        Arguments = "-output-directory=bin -halt-on-error index.tex"
                                    }
                                }
                            }
                        }
                    },
                    new Makefile()
                    {
                        Targets = new List<MakeTarget>()
                        {
                            new MakeTarget()
                            {
                                Name = "all",
                                Dependencies = new List<string>()
                                {
                                    "bin/index.pdf"
                                }
                            },
                            new MakeTarget()
                            {
                                Name = "clean",
                                IsPhony = true,
                                Commands = new List<string>()
                                {
                                    "rm -rf bin"
                                }
                            },
                            new MakeTarget()
                            {
                                Name = "bin",
                                IsPhony = false,
                                Commands = new List<string>()
                                {
                                    "mkdir bin"
                                }
                            },
                            new MakeTarget()
                            {
                                Name = "bin/index.pdf",
                                IsPhony = false,
                                Dependencies = new List<string>()
                                {
                                    "index.tex"
                                },
                                OrderOnlyDependencies = new List<string>()
                                {
                                    "bin"
                                },
                                Commands = new List<string>()
                                {
                                    "pdflatex -output-directory=bin -halt-on-error index.tex",
                                    "pdflatex -output-directory=bin -halt-on-error index.tex"
                                }
                            }
                        },
                    }
                };
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return SingleProject();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(Data))]
        public void Write(ProjectTask task, Makefile expected)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            Assert.Equal(expected, task.GetMakefile());
        }
    }
}
