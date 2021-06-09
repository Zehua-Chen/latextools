using System.Collections.Generic;

namespace LaTeXTools.Build.Log
{
    public class MemoryLogger : ILogger
    {
        // public List<string> Errors = new List<string>();
        // public List<string> Logs = new List<string>();
        // public List<string> Message = new List<string>();

        public void LogError(string error)
        {
            throw new System.NotImplementedException();
        }

        public void LogFile(string log)
        {
            throw new System.NotImplementedException();
        }

        public void Log(string message)
        {
            throw new System.NotImplementedException();
        }

        public void LogProcessStdErr(string invocation, string stderr)
        {
            throw new System.NotImplementedException();
        }

        public void LogProcessStdOut(string invocation, string stdout)
        {
            throw new System.NotImplementedException();
        }
    }
}
