using System;
using System.Linq;

namespace Merry.Christmas
{
    public class Day1Part2 : PuzzleSolver
    {
        protected override void Run(string[] inputLines)
        {
            var depths = inputLines.Select(s => Convert.ToInt32(s)).ToArray();
            var depthIncreases = 0;
            for(var i=3; i < inputLines.Length; i++)
            {
                if(depths[i] > depths[i-3]) depthIncreases++;
            }
            Console.WriteLine(depthIncreases);
        }
    }
}