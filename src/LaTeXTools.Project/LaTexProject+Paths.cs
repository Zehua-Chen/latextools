using System.Collections.Generic;
using System.IO;

namespace LaTeXTools.Project
{
    public static class LaTeXProjectPaths
    {
        public static string GetAUX(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.aux", document));
        }

        public static string GetPDF(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.pdf", document));
        }

        public static List<string> GetDependencies(this LaTeXProject project)
        {
            var dependencies = new List<string>();

            dependencies.Add(project.Entry);
            dependencies.AddRange(project.Includes);

            return dependencies;
        }

        public static LaTeXBackend GetBackend(this LaTeXProject project)
        {
            var backend = LaTeXBackend.Create(project.LaTex);
            backend.OutputDirectory = project.Bin;
            backend.Entry = project.Entry;

            return backend;
        }
    }
}
