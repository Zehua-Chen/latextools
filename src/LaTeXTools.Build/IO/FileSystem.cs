using System;
using System.Collections.Generic;
using System.IO;

namespace LaTeXTools.Build.IO
{
    public sealed class DirectoryOperations : IDirectoryOperations
    {
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            return Directory.EnumerateFileSystemEntries(path);
        }
    }

    public class FileOperations : IFileOperations
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            return new FileInfo(path).LastWriteTimeUtc;
        }

        public Stream Open(string path, FileMode fileMode)
        {
            return File.Open(path, fileMode);
        }
    }

    public sealed class FileSystem : IFileSystem
    {
        public IDirectoryOperations Directory { get; init; } = new DirectoryOperations();
        public IFileOperations File { get; init; } = new FileOperations();
    }
}
