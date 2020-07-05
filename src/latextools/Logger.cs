using System;
using LaTeXTools.Build.Log;

namespace latextools
{
    public class Logger : ILogger
    {
        public void LogAction(string action)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"==> {action}");

            Console.ForegroundColor = originalColor;
        }

        public void LogStdOut(string action, string stdout)
        {
            if (stdout.Length == 0)
            {
                return;
            }

            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"==> {action}");

            Console.ForegroundColor = originalColor;
            Console.WriteLine(stdout);
        }

        public void LogStdErr(string action, string stderr)
        {
            if (stderr.Length == 0)
            {
                return;
            }

            Console.WriteLine(stderr.Length);

            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"==> {action}");

            Console.ForegroundColor = originalColor;
            Console.WriteLine(stderr);
        }
    }
}
