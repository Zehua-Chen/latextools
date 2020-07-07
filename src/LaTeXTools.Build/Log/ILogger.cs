namespace LaTeXTools.Build.Log
{
    /// <summary>
    /// Logger used to report process of the build process
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Called when an action happens
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="title">the action</param>
        void LogMessage(string title);

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
