using System;
using System.IO;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Tasks
{
    /// <summary>
    /// Compare if the current content of a file is the same as the original content of a file
    /// </summary>
    public sealed class FileContentComparison : IEquatable<FileContentComparison>
    {
        public string Path { get; set; }
        public string OriginalContent { get; set; }

        /// <summary>
        /// Create a new file comparison
        /// </summary>
        /// <param name="file">the path to the file</param>
        /// <param name="originalContent">original content of the file</param>
        public FileContentComparison(string path, string originalContent)
        {
            Path = path;
            OriginalContent = originalContent;
        }

        public static async ValueTask<FileContentComparison> OpenAsync(string path)
        {
            return new FileContentComparison(path, await ReadAsync(path));
        }

        /// <summary>
        /// Read the file's contents into a string
        /// </summary>
        /// <param name="path">the path to read from</param>
        /// <returns>
        /// the content if file exists; if the file does not exist, return empty string
        /// </returns>
        private static async ValueTask<string> ReadAsync(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }

            using FileStream fileStream = File.Open(path, FileMode.Open);
            using var reader = new StreamReader(fileStream);

            string fileContent = await reader.ReadToEndAsync();

            return fileContent;
        }

        /// <summary>
        /// Read the file and determines if the content is the same
        /// </summary>
        /// <returns>true if the content is the same</returns>
        public async ValueTask<bool> CompareAsync()
        {
            string newContent = await ReadAsync(Path);

            return newContent == OriginalContent;
        }

        public bool Equals(FileContentComparison? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Path == Path && other.OriginalContent == OriginalContent;
        }
    }
}
