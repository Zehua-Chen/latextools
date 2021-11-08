using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async ValueTask LogError(string error)
        {
            await _semaphore.WaitAsync();
            Errors.Add(error);
            _semaphore.Release();
        }

        public async ValueTask LogFile(string file)
        {
            await _semaphore.WaitAsync();
            Files.Add(file);
            _semaphore.Release();
        }

        public async ValueTask Log(string message)
        {
            await _semaphore.WaitAsync();
            Messages.Add(message);
            _semaphore.Release();
        }

        public async ValueTask LogProcessStdErr(ProcessOutput stderr)
        {
            await _semaphore.WaitAsync();
            ProcessStdErrs.Add(stderr);
            _semaphore.Release();
        }

        public async ValueTask LogProcessStdOut(ProcessOutput stdout)
        {
            await _semaphore.WaitAsync();
            ProcessStdOuts.Add(stdout);
            _semaphore.Release();
        }
    }
}
