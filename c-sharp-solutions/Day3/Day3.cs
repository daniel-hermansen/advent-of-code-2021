using System;
using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas
{
    public class Day3 : PuzzleSolver
    {
        protected override void Run(string[] inputLines)
        {
            var bitmap = new Dictionary<int, (int falseCount, int trueCount)>();
            foreach (var line in inputLines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char ch = line[i];
                    var currentValues = ((ch == '0' ? 1 : 0),ch == '1' ? 1 : 0);
                    if (bitmap.ContainsKey(i))
                    {
                        bitmap[i] = (bitmap[i].falseCount + currentValues.Item1, bitmap[i].trueCount + currentValues.Item2);
                    }
                    else
                    {
                        bitmap[i] = currentValues;
                    }
                    
                }
            }
            int gamma = 0;
            int epsilon = 0;
            foreach (var bit in bitmap)
            {
                gamma = gamma << 1;
                epsilon = epsilon << 1;
                if (bit.Value.trueCount > bit.Value.falseCount)
                {
                    gamma += 1;
                    Console.Write("1");
                }
                else
                {
                    epsilon += 1;
                    Console.Write("0");
                }
            }
            Console.WriteLine();
            Console.WriteLine(gamma);
            Console.WriteLine(epsilon);
            Console.WriteLine($"Final answer: {gamma*epsilon}");
        }
    }
}