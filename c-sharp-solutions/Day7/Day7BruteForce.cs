using System;
using System.Linq;

namespace Merry.Christmas.Day7
{
    public class Day7BruteForce : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var input = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            return Enumerable.Range(0, input.Length).Min(x => input.Sum(p => GetDistance(p, x))).ToString();
        }

        protected virtual int GetDistance(int p, int x) => Math.Abs(p - x);
    }

    public class Day7Part2BruteForce : Day7BruteForce
    {
        protected override int GetDistance(int p, int x)
        {
            var distance = base.GetDistance(p, x);
            return (1 + distance) * distance / 2;
        }
    }
}