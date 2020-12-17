using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LaTeXTools.Build.Generators
{
    /// <summary>
    /// A target in the make
    /// </summary>
    public sealed class MakeTarget : IEquatable<MakeTarget>
    {
        /// <summary>
        /// The name of the target
        /// </summary>
        public string Name { get; set; } = "target";

        /// <summary>
        /// If the target is phony
        /// </summary>
        public bool IsPhony { get; set; } = false;

        /// <summary>
        /// The commands of the target
        /// </summary>
        public List<string> Commands { get; set; } = new List<string>();

        /// <summary>
        /// The dependencies of the target
        /// </summary>
        public List<string> Dependencies { get; set; } = new List<string>();

        /// <summary>
        /// The order-only dependencies of the target. Order-only dependencies woule be written
        /// after targets, with a <c>|</c> before them
        /// </summary>
        public List<string> OrderOnlyDependencies { get; set; } = new List<string>();

        public bool Equals(MakeTarget? other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name
                && IsPhony == other.IsPhony
                && Commands.SequenceEqual(other.Commands)
                && Dependencies.SequenceEqual(other.Dependencies)
                && OrderOnlyDependencies.SequenceEqual(other.OrderOnlyDependencies);
        }
    }

    /// <summary>
    /// Implement <c>MakeTarget</c> extensions on <c>TextWriter</c>
    /// </summary>
    public static class TextWriterMakeTargetExtensions
    {
        /// <summary>
        /// Write a make target
        /// </summary>
        /// <param name="writer">the responsible writer</param>
        /// <param name="target">the target to write</param>
        public static void WriteMakeTarget(this TextWriter writer, MakeTarget target)
        {
            bool finishWithEndLine = false;

            if (target.IsPhony)
            {
                writer.WriteLine($".PHONY: {target.Name}");
            }

            writer.Write($"{target.Name}:");

            if (target.Dependencies.Count > 0)
            {
                writer.Write(" ");

                string dependencies = string.Join(" ", target.Dependencies);
                writer.Write(dependencies);
            }

            if (target.OrderOnlyDependencies.Count > 0)
            {
                string orderOnlyDependencies = string.Join(" ", target.OrderOnlyDependencies);
                writer.Write($" | {orderOnlyDependencies}");
            }

            if (target.Commands.Count > 0)
            {
                writer.WriteLine();

                foreach (var command in target.Commands)
                {
                    writer.WriteLine($"\t{command}");
                }

                finishWithEndLine = true;
            }

            if (!finishWithEndLine)
            {
                writer.WriteLine();
            }
        }
    }
}
