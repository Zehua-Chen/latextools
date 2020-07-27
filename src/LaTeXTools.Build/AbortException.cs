using System;

namespace LaTeXTools.Build
{
    public class AbortException : Exception
    {
        public int ExitCode { get; set; }
    }
}
