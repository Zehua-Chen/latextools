using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

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

        [JsonPropertyName("bib")]
        public string Bib { get; set; } = "none";

        [JsonPropertyName("includes")]
        public string[] Includes { get; set; } = new string[] { };

        [JsonIgnore]
        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        public async ValueTask WriteAsync(string path)
        {
            using FileStream file = File.OpenWrite(path);
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            await JsonSerializer.SerializeAsync(file, this, options);
        }

        public static async ValueTask<LaTeXProject?> FindAsync(string filename)
        {
            string current = Environment.CurrentDirectory;
            string root = Path.GetPathRoot(current)!;

            while (!File.Exists(Path.Combine(current, filename)) || current == root)
            {
                string? parent = Path.GetDirectoryName(current);

                if (parent == null)
                {
                    return null;
                }

                current = parent;
            }

            string path = Path.Combine(current, filename);

            if (File.Exists(path))
            {
                return await LoadAsync(path);
            }

            return null;
        }

        public static async ValueTask<LaTeXProject> LoadAsync(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException($"{path} is not rooted!");
            }

            using var stream = File.Open(path, FileMode.Open);
            string directory = Path.GetDirectoryName(path)!;

            var project = await JsonSerializer.DeserializeAsync<LaTeXProject>(stream);

            if (project == null)
            {
                throw new Exception($"Failed to parse project at {path}");
            }

            project.WorkingDirectory = directory;

            switch (project.Bib)
            {
                case "none":
                case "biber":
                    break;
                default:
                    throw new ArgumentException("\"bib\" can only be none or biber");
            }

            return project;
        }
    }
}
