using System;
using System.Linq;

namespace Merry.Christmas.Day6
{
    public class Day6 : PuzzleSolver
    {
        protected int TotalDays { get; set; } = 80;

        protected override string Solve(string[] inputLines)
        {
            var input = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var fishByAge = new long[9]; // new lanternfish will start at index 8
            foreach (var fishAge in input) fishByAge[fishAge]++; // initial seed fish
            for (var day = 0; day < TotalDays; day++)
            {
                var newFish = (day + 7) % 9;
                var oldFish = day % 9;
                fishByAge[newFish] += fishByAge[oldFish]; // circular shift register
            }
            return fishByAge.Sum().ToString();
        }
    }

    public class Day6Part2 : Day6
    {
        public Day6Part2() { TotalDays = 256; }
    }
}
