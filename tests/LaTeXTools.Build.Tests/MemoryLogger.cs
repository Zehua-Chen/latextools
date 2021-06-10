using System.Collections.Generic;
using LaTeXTools.Build.Log;

namespace LaTeXTools.Build.Tests
{
    public class MemoryLogger : ILogger
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public List<string> Files { get; private set; } = new List<string>();
        public List<string> Messages { get; private set; } = new List<string>();
        public List<ProcessOutput> ProcessStdOuts { get; private set; } = new List<ProcessOutput>();
        public List<ProcessOutput> ProcessStdErrs { get; private set; } = new List<ProcessOutput>();

        public void LogError(string error)
        {
            lock (this)
            {
                Errors.Add(error);
            }
        }

        public void LogFile(string file)
        {
            lock (this)
            {
                Files.Add(file);
            }

        }

        public void Log(string message)
        {
            lock (this)
            {
                Messages.Add(message);
            }
        }

        public void LogProcessStdErr(in ProcessOutput stderr)
        {
            lock (this)
            {
                ProcessStdErrs.Add(stderr);
            }
        }

        public void LogProcessStdOut(in ProcessOutput stdout)
        {
            lock (this)
            {
                ProcessStdOuts.Add(stdout);
            }
        }
    }
}
