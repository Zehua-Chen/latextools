using System.Collections.Generic;
using LaTeXTools.Build.Tasks;
using Xunit;

namespace LaTeXTools.Build.Tests.Tasks
{
    public class FileContentComparisonTests
    {
        [Theory]
        [MemberData(nameof(EqualData))]
        public void Equal(FileContentComparison a, FileContentComparison b)
        {
            Assert.Equal(a, b);
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void NotEqual(FileContentComparison a, FileContentComparison b)
        {
            Assert.NotEqual(a, b);
        }

        public static IEnumerable<object[]> EqualData() => new List<object[]>
        {
            new object[]
            {
                new FileContentComparison("a", "b"),
                new FileContentComparison("a", "b")
            }
        };

        public static IEnumerable<object[]> NotEqualData() => new List<object[]>
        {
            new object[]
            {
                new FileContentComparison("a", "a"),
                new FileContentComparison("a", "b")
            },
            new object[]
            {
                new FileContentComparison("a", "b"),
                new FileContentComparison("b", "b")
            }
        };
    }
}
