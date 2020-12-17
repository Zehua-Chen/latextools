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
        void Message(string message);

        /// <summary>
        /// log error
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="error">the error</param>
        void Error(string error);

        /// <summary>
        /// Log log produced by another process
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="log">the log content</param>
        void Log(string log);

        /// <summary>
        /// Called to output a stdout of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the process's invocation</param>
        /// <param name="stdout">the stdout</param>
        void StdOut(string invocation, string stdout);

        /// <summary>
        /// Called to output the stderr of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the subprocess's invocation</param>
        /// <param name="stderr">the stderr</param>
        void StdErr(string invocation, string stderr);
    }
}
