using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace LaTeXTools.Build.Generators
{
    public class Makefile : IEquatable<Makefile>
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

        public bool Equals(Makefile? other)
        {
            if (other == null)
            {
                return false;
            }

            return TopLevelComment == other.TopLevelComment
                && Enumerable.SequenceEqual(Targets, other.Targets);
        }
    }
}
