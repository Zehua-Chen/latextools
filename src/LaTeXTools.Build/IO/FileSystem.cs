using System.IO;

namespace LaTeXTools.Build.IO
{
    public class FileSystem : IFileSystem
    {
        public Stream Open(string path, FileMode fileMode)
        {
            return File.Open(path, fileMode);
        }
    }
}
