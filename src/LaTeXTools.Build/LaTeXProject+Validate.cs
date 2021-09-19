using System;
using System.IO;
using System.IO.Abstractions;
using LaTeXTools.Project;

namespace LaTeXTools.Build
{
    public static class LaTeXProjectValidateExtensions
    {
        /// <summary>
        /// Throw if the project is invalid
        /// </summary>
        /// <param name="project"></param>
        /// <param name="fileSystem"></param>
        public static void ThrowIfInvalid(this LaTeXProject project, IFileSystem fileSystem)
        {
            project.ThrowIfBibNotSupported();
            project.ThrowIfDependenciesNotFound(fileSystem);
        }

        /// <summary>
        /// Throw an exception if a bibliography is not suppported
        /// </summary>
        /// <param name="project"></param>
        public static void ThrowIfBibNotSupported(this LaTeXProject project)
        {
            switch (project.Bib)
            {
                case "none":
                case "biber":
                    break;
                default:
                    throw new ArgumentException("\"bib\" can only be none or biber");
            }
        }

        /// <summary>
        /// Throw exception if a dependency does not exists
        /// </summary>
        /// <param name="project"></param>
        public static void ThrowIfDependenciesNotFound(this LaTeXProject project, IFileSystem fileSystem)
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
