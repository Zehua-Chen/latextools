using System;
using System.IO.Abstractions;
using LaTeXTools.Build;

namespace LaTeXTools.CLI
{
    public static class LaTeXBuildValidateExtensions
    {
        public static bool CanBuild(this LaTeXBuild build, IFileSystem fileSystem, out string error)
        {
            error = "";

            try
            {
                build.Root.PathsExist(fileSystem);
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return false;
        }
    }
}
