using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;

namespace LaTeXTools.Project
{
    public class LaTeXProject
    {
        [JsonPropertyName("latex")]
        public string LaTex { get; set; } = "pdflatex";

        [JsonPropertyName("bin")]
        public string Bin { get; set; } = "bin";

        [JsonPropertyName("entry")]
        public string Entry { get; set; } = "index.tex";

        [JsonPropertyName("includes")]
        public string[] Includes { get; set; } = new string[] { };

        [JsonIgnore]
        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        public static async ValueTask<LaTeXProject> FindAsync(
            string filename,
            LaTeXProject @default)
        {
            string current = Environment.CurrentDirectory;
            string root = Path.GetPathRoot(current);

            while (!File.Exists(Path.Combine(current, filename)) || current == root)
            {
                current = Path.GetDirectoryName(current);
            }

            string path = Path.Combine(current, filename);

            if (File.Exists(path))
            {
                return await LoadAsync(path);
            }

            return @default;
        }

        public static async ValueTask<LaTeXProject> LoadAsync(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException($"{path} is not rooted!");
            }

            using var stream = File.Open(path, FileMode.Open);
            string directory = Path.GetDirectoryName(path);

            var project = await JsonSerializer.DeserializeAsync<LaTeXProject>(stream);

            project.WorkingDirectory = directory;
            project.Bin = Path.GetFullPath(project.Bin, directory);
            project.Entry = Path.GetFullPath(project.Entry, directory);
            project.Includes = project.Includes
                .Select((current) => { return Path.GetFullPath(current, directory); })
                .ToArray();

            return project;
        }
    }
}
