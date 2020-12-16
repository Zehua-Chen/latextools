using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaTeXTools.Build.Generators
{
    public class MakeTarget : IEquatable<MakeTarget>
    {
        public string Name { get; set; } = "target";
        public bool IsPhony { get; set; } = false;
        public List<string> Commands { get; set; } = new List<string>();
        public List<string> Dependencies { get; set; } = new List<string>();
        public List<string> OrderOnlyDependencies { get; set; } = new List<string>();

        public async ValueTask WriteAsync(TextWriter writer)
        {
            bool finishWithEndLine = false;

            if (this.IsPhony)
            {
                await writer.WriteLineAsync($".PHONY: {this.Name}");
            }

            await writer.WriteAsync($"{this.Name}:");

            if (this.Dependencies.Count > 0)
            {
                await writer.WriteAsync(" ");
                string dependencies = string.Join(" ", this.Dependencies);
                await writer.WriteAsync(dependencies);
            }

            if (this.OrderOnlyDependencies.Count > 0)
            {
                string orderOnlyDependencies = string.Join(" ", this.OrderOnlyDependencies);
                await writer.WriteAsync($" | {orderOnlyDependencies}");
            }

            if (this.Commands.Count > 0)
            {
                await writer.WriteLineAsync();

                foreach (var command in this.Commands)
                {
                    await writer.WriteLineAsync($"\t{command}");
                }

                finishWithEndLine = true;
            }

            if (!finishWithEndLine)
            {
                await writer.WriteLineAsync();
            }
        }

        public bool Equals(MakeTarget? other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name
                && IsPhony == other.IsPhony
                && Enumerable.SequenceEqual(Commands, other.Commands)
                && Enumerable.SequenceEqual(Dependencies, other.Dependencies)
                && Enumerable.SequenceEqual(OrderOnlyDependencies, other.OrderOnlyDependencies);
        }
    }
}
