using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaTeXTools.Build
{
    internal class BuildGraph
    {
        private class Node
        {
        }

        private class FileNode : Node
        {
        }

        private class OutputDirectoryNode : Node
        {

        }

        internal ValueTask<int> Run()
        {
            return new ValueTask<int>();
        }
    }
}
