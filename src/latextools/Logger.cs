using System;
using LaTeXTools.Build.Log;

namespace latextools
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

        public void Log(string action)
        {
            lock (this)
            {
                using ConsoleColorGuard guard = ConsoleColorGuard.Current;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"==> {action}");
            }
        }

        public void LogError(string error)
        {
            lock (this)
            {
                using ConsoleColorGuard guard = ConsoleColorGuard.Current;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"==> {error}");
            }
        }

        public void LogFile(string log)
        {
            using ConsoleColorGuard guard = ConsoleColorGuard.Current;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{log}");
        }

        public void LogProcessStdOut(in ProcessOutput stdout)
        {
            if (stdout.Message.Length == 0)
            {
                return;
            }

            lock (this)
            {
                // using ConsoleColorGuard guard = ConsoleColorGuard.Current;
                Console.WriteLine(stdout.Invocation);
                Console.WriteLine();

                Console.WriteLine(stdout.Message);
                Console.WriteLine();
            }
        }

        public void LogProcessStdErr(in ProcessOutput stderr)
        {
            if (stderr.Message.Length == 0)
            {
                return;
            }

            lock (this)
            {
                using ConsoleColorGuard guard = ConsoleColorGuard.Current;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(stderr.Invocation);
                Console.WriteLine();

                Console.WriteLine(stderr.Message);
                Console.WriteLine();
            }
        }
    }
}
