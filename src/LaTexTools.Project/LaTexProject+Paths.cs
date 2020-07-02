using System.Collections.Generic;
using System.IO;

namespace LaTexTools.Project
{
    public static class LaTexProjectPaths
    {
        public static string GetAUX(this LaTexProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.aux", document));
        }

        public static string GetPDF(this LaTexProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.pdf", document));
        }

        public static List<string> GetDependencies(this LaTexProject project)
        {
            var dependencies = new List<string>();

            dependencies.Add(project.Entry);

            foreach (string include in project.Includes)
            {
                dependencies.Add(Path.Combine(project.Bin, include));
            }

            return dependencies;
        }
    }
}
