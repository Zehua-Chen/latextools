using System;
using System.IO;
using LaTeXTools.Project;

namespace LaTeXTools.Build
{
    public static class LaTeXProjectValidate
    {
        public static void Validate(this LaTeXProject project)
        {
            if (!File.Exists(project.Entry))
            {
                throw new FileNotFoundException($"{project.Entry} not found");
            }

            foreach (var include in project.Includes)
            {
                if (!File.Exists(include) && !Directory.Exists(include))
                {
                    throw new Exception($"{include} not found");
                }
            }
        }
    }
}
