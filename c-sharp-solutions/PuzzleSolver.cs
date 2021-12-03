using System;
using System.IO;

namespace Merry.Christmas
{
    /// <summary> Shared base class for all puzzle solutions </summary>
    public abstract class PuzzleSolver
    {
        public virtual void Run(string inputFilepath)
        {
            if (!File.Exists(inputFilepath))
            {
                Console.WriteLine($"The puzzle input file could not be found at '{inputFilepath}'");
                return;
            }
            var inputLines = File.ReadAllLines(inputFilepath);
            Run(inputLines);
        }

        protected abstract void Run(string[] inputLines);
    }
}