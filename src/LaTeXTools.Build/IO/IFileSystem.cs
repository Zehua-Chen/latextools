using System;
using System.IO;

namespace LaTeXTools.Build.IO
{
    public interface IFileSystem
    {
        void CreateDirectory(string path);
        bool DirectoryExists(string path);

        bool FileExists(string path);
        DateTime GetFileLastWriteTimeUtc(string path);
        Stream Open(string path, FileMode fileMode);
    }
}
