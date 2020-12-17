using System.IO;
using System.Diagnostics;

namespace LaTeXTools.Project
{
    public static class LaTeXProjectProcessExtensions
    {
        public static ProcessStartInfo GetLaTeXStartInfo(this LaTeXProject project)
        {
            return new ProcessStartInfo()
            {
                FileName = project.LaTeX,
                Arguments = $"-output-directory={project.Bin} -interaction=batchmode {project.Entry}",
            };
        }

        public static ProcessStartInfo GetBibStartInfo(this LaTeXProject project)
        {
            string projectName = Path.GetFileNameWithoutExtension(project.Entry);
            string bibPath = Path.Combine(project.Bin, projectName);

            return new ProcessStartInfo()
            {
                FileName = project.Bib,
                Arguments = $"{bibPath}"
            };
        }
    }
}
