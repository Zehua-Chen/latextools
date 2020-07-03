using System.Diagnostics;

namespace LaTeXTools.Project
{
    public static class LaTexProjectProcess
    {
        public static ProcessStartInfo GetStartInfo(this LaTeXProject project)
        {
            return new ProcessStartInfo()
            {
                FileName = project.LaTex,
                Arguments = $"-output-directory={project.Bin} {project.Entry}",
            };
        }
    }
}
