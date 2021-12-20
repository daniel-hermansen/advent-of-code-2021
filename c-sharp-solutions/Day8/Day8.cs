using System.Linq;

namespace Merry.Christmas.Day8
{
    public class Day8 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            return inputLines
                .SelectMany(l => l.Split(" | ")[1].Split(" "))
                .Count(u => u.Length is 2 or 3 or 4 or 7).ToString();
        }
    }
}
