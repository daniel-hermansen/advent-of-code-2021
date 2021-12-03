using System;
using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas
{
    public class Day2 : PuzzleSolver
    {
        protected override void Run(string[] inputLines)
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
            Console.WriteLine(horizontal*depth);
        }
    }
}