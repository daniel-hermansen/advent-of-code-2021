using System;
using System.Linq;

namespace Merry.Christmas
{
    public class Day1 : PuzzleSolver
    {
        protected override void Run(string[] inputLines)
        {
            var depths = inputLines.Select(s => Convert.ToInt32(s)).ToArray();
            int depthIncreases = 0;
            for (int i = 1; i < depths.Length; i++)
            {
                if (depths[i] > depths[i-1]) depthIncreases++;
            }
            Console.WriteLine(depthIncreases);
        }
    }
}