using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas
{
    public class Day2 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            IEnumerable<(string direction, int value)> input = inputLines.Select(s =>
                                            {
                                                var split = s.Split(" ");
                                                return (split[0], int.Parse(split[1]));
                                            });
            int horizontal = 0;
            int depth = 0;
            foreach (var element in input)
            {
                switch (element.direction)
                {
                    case "forward":
                        horizontal += element.value;
                        break;
                    case "up":
                        depth -= element.value;
                        break;
                    case "down":
                        depth += element.value;
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }
    }

    public class Day2Part2 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            IEnumerable<(string direction, int x)> input = inputLines.Select(s =>
                                            {
                                                var split = s.Split(" ");
                                                return (split[0], int.Parse(split[1]));
                                            });
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var element in input)
            {
                switch (element.direction)
                {
                    case "forward":
                        horizontal += element.x;
                        depth += aim * element.x;
                        break;
                    case "up":
                        aim -= element.x;
                        break;
                    case "down":
                        aim += element.x;
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}