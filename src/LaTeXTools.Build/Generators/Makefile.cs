using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Generators
{
    public class Makefile
    {
        public string TopLevelComment { get; set; } = "# Generated Makefile";
        public List<MakeTarget> Targets { get; set; } = new List<MakeTarget>();

        public async ValueTask WriteAsync(TextWriter writer)
        {
            await writer.WriteLineAsync(TopLevelComment);
            await writer.WriteLineAsync();

            foreach (var target in this.Targets)
            {
                await target.WriteAsync(writer);
                await writer.WriteLineAsync();
            }
        }
    }
}
