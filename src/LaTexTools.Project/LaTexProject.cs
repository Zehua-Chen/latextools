using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace LaTexTools.Project
{
    public class LaTexProject
    {
        [JsonPropertyName("latex")]
        public string LaTex { get; set; } = "pdflatex";

        [JsonPropertyName("bin")]
        public string Bin { get; set; } = "bin";

        [JsonPropertyName("entry")]
        public string Entry { get; set; } = "index.tex";

        [JsonPropertyName("includes")]
        public string[] Includes { get; set; } = new string[] {};

        public static ValueTask<LaTexProject> LoadAsync(string filename)
        {
            using var stream = new FileStream(filename, FileMode.Open);
            return JsonSerializer.DeserializeAsync<LaTexProject>(stream);
        }
    }
}
