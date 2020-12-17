using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace LaTeXTools.Build.Generators
{
    /// <summary>
    /// A make file
    /// </summary>
    public sealed class Makefile : IEquatable<Makefile>
    {
        /// <summary>
        /// Top level comment, which would be printed as comment on the the first line of the
        /// makefile
        /// </summary>
        /// <value></value>
        public string TopLevelComment { get; set; } = "Generated Makefile";

        /// <summary>
        /// Targets in the makefile.
        /// </summary>
        /// <see cref="MakeTarget"></see>
        public List<MakeTarget> Targets { get; set; } = new List<MakeTarget>();

        public bool Equals(Makefile? other)
        {
            if (other == null)
            {
                return false;
            }

            return TopLevelComment == other.TopLevelComment
                && Targets.SequenceEqual(other.Targets);
        }
    }

    /// <summary>
    /// Implement <c>Makefile</c> extensions on <c>TextWriter</c>
    /// </summary>
    public static class TextWriterMakefileExtensions
    {
        /// <summary>
        /// Write a makefile
        /// </summary>
        /// <param name="writer">the writer responsible for writing</param>
        /// <param name="makefile">the makefile</param>
        public static void WriteMakefile(this TextWriter writer, Makefile makefile)
        {
            writer.WriteLine($"# {makefile.TopLevelComment}");
            writer.WriteLine();

            foreach (var target in makefile.Targets)
            {
                writer.WriteMakeTarget(target);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Write a makefile async
        /// </summary>
        /// <param name="writer">the writer responsible for writing</param>
        /// <param name="makefile">the makefile</param>
        /// <returns>a task</returns>
        public static ValueTask WriteMakefileAsync(this TextWriter writer, Makefile makefile)
        {
            return new ValueTask(Task.Run(() => writer.WriteMakefile(makefile)));
        }
    }
}
