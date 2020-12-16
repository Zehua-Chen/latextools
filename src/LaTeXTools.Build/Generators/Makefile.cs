using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Generators
{
    public class Makefile
    {
        public List<MakeTarget> Targets { get; private set; } = new List<MakeTarget>();

        public async ValueTask Write(string path)
        {
            using StreamWriter writer = File.CreateText(path);

            await writer.WriteLineAsync("# Generated Makefile");
            await writer.WriteLineAsync();

            foreach (var target in this.Targets)
            {
                await target.Write(writer);
                await writer.WriteLineAsync();
            }
        }
    }
}
