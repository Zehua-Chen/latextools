using System;
using LaTeXTools.Build.Log;

namespace latextools
{
    public class Logger : ILogger
    {
        public void LogAction(string action)
        {
            lock (this)
            {
                var originalColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"==> {action}");

                Console.ForegroundColor = originalColor;
            }
        }

        public void LogStdOut(string action, string stdout)
        {
            if (stdout.Length == 0)
            {
                return;
            }

            lock (this)
            {
                var originalColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"==> {action}");

                Console.ForegroundColor = originalColor;
                Console.WriteLine(stdout);
            }
        }

        public void LogStdErr(string action, string stderr)
        {
            if (stderr.Length == 0)
            {
                return;
            }

            lock (this)
            {
                Console.WriteLine(stderr.Length);

                var originalColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"==> {action}");

                Console.ForegroundColor = originalColor;
                Console.WriteLine(stderr);
            }
        }
    }
}
