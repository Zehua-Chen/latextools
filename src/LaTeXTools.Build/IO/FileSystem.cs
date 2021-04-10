using System;
using System.IO;

namespace LaTeXTools.Build.IO
{
    public sealed class FileSystem : IFileSystem
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public DateTime GetFileLastWriteTimeUtc(string path)
        {
            return new FileInfo(path).LastWriteTimeUtc;
        }

        public Stream Open(string path, FileMode fileMode)
        {
            return File.Open(path, fileMode);
        }
    }
}
