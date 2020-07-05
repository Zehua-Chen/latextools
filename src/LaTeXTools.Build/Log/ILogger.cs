namespace LaTeXTools.Build.Log
{
    public interface ILogger
    {
        void LogAction(string action);
        void LogStdOut(string action, string message);
        void LogStdErr(string action, string message);
    }
}
