using System.Collections.Generic;
using System.Diagnostics;
using LaTeXTools.Build.Tasks;

namespace LaTeXTools.Build.Generators
{
    /// <summary>
    /// Generates makefile for a given project task
    /// </summary>
    public static class ProjectTaskMakeExtensions
    {
        public static Makefile GetMakefile(this ProjectTask projectTask)
        {
            Makefile make = new Makefile();

            var all = new MakeTarget();
            all.Name = "all";
            all.Dependencies.Add(projectTask.OutputPDFPath);
            make.Targets.Add(all);

            HandleProject(make, projectTask);

            return make;
        }

        private static void HandleProject(Makefile make, ProjectTask projectTask)
        {
            make.Targets.Add(GetCleanTarget(projectTask));
            make.Targets.Add(GetBinTarget(projectTask));

            var pdfTarget = GetPDFTarget(projectTask);
            make.Targets.Add(pdfTarget);

            if (projectTask.SubProjects != null)
            {
                foreach (var children in projectTask.SubProjects)
                {
                    HandleProject(make, children);
                    pdfTarget.Dependencies.Add(projectTask.OutputPDFPath);
                }
            }
        }

        private static MakeTarget GetCleanTarget(ProjectTask projectTask)
        {
            return new MakeTarget()
            {
                IsPhony = true,
                Name = "clean",
                Commands = new List<string>()
                {
                    $"rm -rf {projectTask.OutputDirectory}"
                }
            };
        }

        private static MakeTarget GetBinTarget(ProjectTask projectTask)
        {
            return new MakeTarget()
            {
                Name = $"{projectTask.OutputDirectory}",
                Commands = new List<string>()
                {
                    $"mkdir {projectTask.OutputDirectory}"
                }
            };
        }

        private static MakeTarget GetPDFTarget(ProjectTask projectTask)
        {
            MakeTarget target = new MakeTarget();

            target.Name = $"{projectTask.OutputPDFPath}";

            if (projectTask.BuildTasks != null)
            {
                foreach (var build in projectTask.BuildTasks)
                {
                    if (build is RunProcessTask)
                    {
                        var runProcessTask = (RunProcessTask)build;
                        ProcessStartInfo? startInfo = runProcessTask.StartInfo;

                        if (startInfo == null)
                        {
                            continue;
                        }

                        target.Commands.Add($"{startInfo.FileName} {startInfo.Arguments}");
                    }
                }
            }

            if (projectTask.DependencyPaths != null)
            {
                target.Dependencies.AddRange(projectTask.DependencyPaths);
            }

            target.OrderOnlyDependencies.Add($"{projectTask.OutputDirectory}");

            return target;
        }
    }
}
