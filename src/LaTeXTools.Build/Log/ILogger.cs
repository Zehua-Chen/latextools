namespace LaTeXTools.Build.Log
{
    /// <summary>
    /// Logger used to report process of the build process
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log message
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="message">the message</param>
        void LogMessage(string message);

        /// <summary>
        /// Log message
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="error">the error</param>
        void LogError(string error);

        /// <summary>
        /// Called to output a stdout of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the process's invocation</param>
        /// <param name="stdout">the stdout</param>
        void LogStdOut(string invocation, string stdout);

        /// <summary>
        /// Called to output the stderr of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the subprocess's invocation</param>
        /// <param name="stderr">the stderr</param>
        void LogStdErr(string invocation, string stderr);
    }
}
