using System.Collections.Generic;
using System.IO;

namespace LaTeXTools.Project
{
    public static class LaTeXProjectPathsExtensions
    {
        public static string GetAUXPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.aux", document));
        }

        public static string GetPDFPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.pdf", document));
        }

        public static string GetLogPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.log", document));
        }

        public static List<string> GetDependencyPaths(this LaTeXProject project)
        {
            var dependencies = new List<string>();

            dependencies.Add(project.Entry);
            dependencies.AddRange(project.Includes);

            return dependencies;
        }
    }
}
