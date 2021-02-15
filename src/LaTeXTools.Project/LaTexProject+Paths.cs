using System.Collections.Generic;
using System.IO;

namespace LaTeXTools.Project
{
    public static class LaTeXProjectPathsExtensions
    {
        /// <summary>
        /// Get the path to <c>.aux</c> file of this project
        /// </summary>
        /// <param name="project">the project in question</param>
        /// <returns>path to the <c>.aux</c> file</returns>
        public static string GetAUXPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.aux", document));
        }

        /// <summary>
        /// Get the path to <c>.gls</c> file of this project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static string GetGLSPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.gls", document));
        }

        /// <summary>
        /// Get the path to <c>.pdf</c> file of this project
        /// </summary>
        /// <param name="project">the project in question</param>
        /// <returns>path to the <c>.pdf</c> file</returns>
        public static string GetPDFPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.pdf", document));
        }

        /// <summary>
        /// Get the path to <c>.log</c> file of this project
        /// </summary>
        /// <param name="project">the project in question</param>
        /// <returns>path to the <c>.log</c> file</returns>
        public static string GetLogPath(this LaTeXProject project)
        {
            string document = Path.GetFileNameWithoutExtension(project.Entry);
            return Path.Combine(project.Bin, string.Format("{0}.log", document));
        }

        /// <summary>
        /// Get a list of the files that are dependencies to building the project. This would
        /// include <c>project.Entry</c>
        /// </summary>
        /// <param name="project">the project in question</param>
        /// <returns>a list of file paths</returns>
        public static List<string> GetDependencyPaths(this LaTeXProject project)
        {
            var dependencies = new List<string>();

            dependencies.Add(project.Entry);
            dependencies.AddRange(project.Includes);

            return dependencies;
        }
    }
}
