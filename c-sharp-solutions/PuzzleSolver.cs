using System;
using System.Diagnostics;
using System.IO;

namespace Merry.Christmas
{
    /// <summary> Shared base class for all puzzle solutions </summary>
    public abstract class PuzzleSolver
    {
        public virtual double Solve(string inputFilepath)
        {
            if (!File.Exists(inputFilepath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The puzzle input file could not be found at '{inputFilepath}'");
                Console.ResetColor();
                return -1;
            }

            var inputLines = File.ReadAllLines(inputFilepath);
            var timer = Stopwatch.StartNew();
            var result = Solve(inputLines);
            timer.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ResetColor();
            Console.WriteLine($"Elapsed: {timer.Elapsed.TotalMilliseconds:N3} ms");
            return timer.Elapsed.TotalMilliseconds;
        }

        protected abstract string Solve(string[] inputLines);
    }
}