using System.IO;

namespace LaTeXTools.Build.IO
{
    public interface IFileSystem
    {
        Stream Open(string path, FileMode fileMode);
    }
}
