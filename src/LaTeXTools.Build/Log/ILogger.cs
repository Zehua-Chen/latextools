using System.Threading.Tasks;

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
        ValueTask LogAsync(string message);

        /// <summary>
        /// log error
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="error">the error</param>
        ValueTask LogErrorAsync(string error);

        /// <summary>
        /// Log log produced by another process
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="log">the log content</param>
        ValueTask LogFileAsync(string log);

        /// <summary>
        /// Called to output a stdout of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the process's invocation</param>
        /// <param name="stdout">the stdout</param>
        ValueTask LogProcessStdOutAsync(string invocation, string stdout)
        {
            return this.LogProcessStdOutAsync(new ProcessOutput { Invocation = invocation, Message = stdout });
        }

        ValueTask LogProcessStdOutAsync(ProcessOutput output);

        /// <summary>
        /// Called to output the stderr of a subprocess
        ///
        /// Must be thread safe
        /// </summary>
        /// <param name="invocation">the subprocess's invocation</param>
        /// <param name="stderr">the stderr</param>
        ValueTask LogProcessStdErrAsync(string invocation, string stderr)
        {
            return this.LogProcessStdOutAsync(new ProcessOutput { Invocation = invocation, Message = stderr });
        }

        ValueTask LogProcessStdErrAsync(ProcessOutput output);
    }
}
