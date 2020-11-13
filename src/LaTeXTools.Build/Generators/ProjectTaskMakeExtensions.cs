using System.Collections.Generic;
using System.Diagnostics;
using LaTeXTools.Build.Tasks;

namespace LaTeXTools.Build.Generators
{
    public static class ProjectTaskMake
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
            make.Targets.Add(ProjectTaskMake.GetCleanTarget(projectTask));
            make.Targets.Add(ProjectTaskMake.GetBinTarget(projectTask));

            var pdfTarget = ProjectTaskMake.GetPDFTarget(projectTask);
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
            MakeTarget target = new MakeTarget();

            target.IsPhony = true;
            target.Name = "clean";
            target.Commands.Add($"rm -rf {projectTask.OutputDirectory}");

            return target;
        }

        private static MakeTarget GetBinTarget(ProjectTask projectTask)
        {
            MakeTarget target = new MakeTarget();

            target.IsPhony = true;
            target.Name = $"{projectTask.OutputDirectory}";
            target.Commands.Add($"mkdir {projectTask.OutputDirectory}");

            return target;
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
