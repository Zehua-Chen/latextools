using System;
using System.Collections.Generic;
using System.IO;

namespace LaTeXTools.Build.IO
{
    public interface IDirectoryOperations
    {
        void Create(string path);
        bool Exists(string path);
        IEnumerable<string> EnumerateFileSystemEntries(string path);
    }

    public interface IFileOperations
    {
        bool Exists(string path);
        DateTime GetLastWriteTimeUtc(string path);
        Stream Open(string path, FileMode fileMode);
    }

    public interface IFileSystem
    {
        IDirectoryOperations Directory { get; }
        IFileOperations File { get; }
    }
}
