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

        [JsonPropertyName("includes")]
        public string[] Includes { get; set; } = new string[] {};

        public static async ValueTask<LaTeXProject> LoadAsync(string filename)
        {
            using var stream = File.Open(filename, FileMode.Open);

            return await JsonSerializer.DeserializeAsync<LaTeXProject>(stream);
        }
    }
}
