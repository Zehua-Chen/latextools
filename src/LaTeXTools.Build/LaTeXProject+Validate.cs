using System;
using System.IO;
using System.IO.Abstractions;
using LaTeXTools.Project;

namespace LaTeXTools.Build
{
    public static class LaTeXProjectValidateExtensions
    {
        /// <summary>
        /// Throw exception if all paths exists
        /// </summary>
        /// <param name="project"></param>
        public static void PathsExist(this LaTeXProject project, IFileSystem fileSystem)
        {
            if (!fileSystem.File.Exists(project.Entry))
            {
                throw new FileNotFoundException($"{project.Entry} not found");
            }

            foreach (var include in project.Includes)
            {
                if (!fileSystem.File.Exists(include) && !fileSystem.Directory.Exists(include))
                {
                    throw new Exception($"{include} not found");
                }
            }
        }
    }
}
