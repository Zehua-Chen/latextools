using System;
using System.Threading;
using System.Threading.Tasks;
using LaTeXTools.Build.Log;

namespace LaTeXTools.CLI
{
    public class Logger : ILogger
    {
        private struct ConsoleColorGuard : IDisposable
        {
            ConsoleColor _foreground;

            public void Dispose()
            {
                Console.ForegroundColor = _foreground;
            }

            public static ConsoleColorGuard Current
            {
                get
                {
                    var guard = new ConsoleColorGuard();
                    guard._foreground = Console.ForegroundColor;

                    return guard;
                }
            }
        }

        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async ValueTask Log(string action)
        {
            await _semaphore.WaitAsync();

            using ConsoleColorGuard guard = ConsoleColorGuard.Current;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"==> {action}");

            _semaphore.Release();
        }

        public async ValueTask LogError(string error)
        {
            await _semaphore.WaitAsync();

            using ConsoleColorGuard guard = ConsoleColorGuard.Current;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"==> {error}");

            _semaphore.Release();
        }

        public async ValueTask LogFile(string log)
        {
            await _semaphore.WaitAsync();

            using ConsoleColorGuard guard = ConsoleColorGuard.Current;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{log}");

            _semaphore.Release();
        }

        public async ValueTask LogProcessStdOut(ProcessOutput stdout)
        {
            if (stdout.Message.Length == 0)
            {
                return;
            }

            await _semaphore.WaitAsync();

            // using ConsoleColorGuard guard = ConsoleColorGuard.Current;
            Console.WriteLine(stdout.Invocation);
            Console.WriteLine();

            Console.WriteLine(stdout.Message);
            Console.WriteLine();

            _semaphore.Release();
        }

        public async ValueTask LogProcessStdErr(ProcessOutput stderr)
        {
            if (stderr.Message.Length == 0)
            {
                return;
            }

            await _semaphore.WaitAsync();

            using ConsoleColorGuard guard = ConsoleColorGuard.Current;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stderr.Invocation);
            Console.WriteLine();

            Console.WriteLine(stderr.Message);
            Console.WriteLine();

            _semaphore.Release();
        }
    }
}
