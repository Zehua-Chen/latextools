using System.Collections.Generic;

namespace LaTeXTools.Build.Log
{
    public class MemoryLogger : ILogger
    {


        public List<string> Errors { get; private set; } = new List<string>();
        public List<string> Files { get; private set; } = new List<string>();
        public List<string> Messages { get; private set; } = new List<string>();
        public List<string> ProcessStdOuts { get; private set; } = new List<string>();
        public List<string> ProcessStdErrs { get; private set; } = new List<string>();

        public void LogError(string error)
        {
            Errors.Add(error);
        }

        public void LogFile(string file)
        {
            Files.Add(file);
        }

        public void Log(string message)
        {
            Messages.Add(message);
        }

        public void LogProcessStdErr(in ProcessOutput stderr)
        {
            throw new System.NotImplementedException();
        }

        public void LogProcessStdOut(in ProcessOutput stdout)
        {
            throw new System.NotImplementedException();
        }
    }
}
