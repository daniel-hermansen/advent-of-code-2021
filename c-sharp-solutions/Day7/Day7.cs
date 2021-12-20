using System;
using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day7
{
    public class Day7 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var input = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

            // Avoid computing the same fuel cost for repeated horizontal position
            var positionToCount = new Dictionary<int, int>();
            foreach (var position in input)
            {
                if (!positionToCount.ContainsKey(position))
                {
                    positionToCount[position] = 1;
                }
                else
                {
                    positionToCount[position] = positionToCount[position] + 1;
                }
            }

            var keys = positionToCount.Keys.ToList();
            // Sort them by horizontal position so we can bail when we start to increment costs
            // Also could binary search this to be even faster
            keys.Sort();
            long minFuel = int.MaxValue;
            
            foreach (var key in keys)
            {
                var totalCost = positionToCount.Sum(x => GetFuelCost(x.Key, x.Value, key));
                if (totalCost < minFuel)
                {
                    minFuel = totalCost;
                }
                else
                {
                    break;
                }
            }

            return minFuel.ToString();
        }

        protected virtual long GetFuelCost(int p, int count, int x) =>  Math.Abs((p - x) * count);
    }

    public class Day7Part2 : Day7
    {
        protected override long GetFuelCost(int p, int count, int x)
        {
            var distance = base.GetFuelCost(p, 1, x);
            return (1 + distance) * distance / 2 * count;
        }
    }
}
