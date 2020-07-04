using System.IO;
using System.Diagnostics;

namespace LaTeXTools.Project
{
    public static class LaTexProjectProcess
    {
        public static ProcessStartInfo GetLaTeXStartInfo(this LaTeXProject project)
        {
            return new ProcessStartInfo()
            {
                FileName = project.LaTex,
                Arguments = $"-output-directory={project.Bin} {project.Entry}",
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
