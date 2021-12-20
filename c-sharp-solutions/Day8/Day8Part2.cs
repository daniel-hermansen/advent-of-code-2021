using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day8
{
    public class Day8Part2 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            int result = 0;
            foreach (var line in inputLines)
            {
                var patternsOutput = line.Split(" | ");
                var patterns = patternsOutput[0].Split(" ").Select(p => p.ToHashSet()).ToArray();
                var output = patternsOutput[1].Split(" ");
                var knownPattern = new HashSet<char>[10];
                foreach (var pattern in patterns)
                {
                    switch (pattern.Count)
                    {
                        case 2:
                            knownPattern[1] = pattern.ToHashSet();
                            break;
                        case 4:
                            knownPattern[4] = pattern.ToHashSet();
                            break;
                        case 3:
                            knownPattern[7] = pattern.ToHashSet();
                            break;
                        case 7:
                            knownPattern[8] = pattern.ToHashSet();
                            break;
                    }

                }

                // All we need to know is how many segments intersect with the '1' pattern or the '4' pattern
                // to determine the patterns for the other digits that don't have unique segment counts
                HashSet<char> FindDigitPattern(int segments, int intersectionsWith1, int intersectionsWith4)
                {
                    return patterns.Single(pattern =>
                        pattern.Count == segments &&
                        pattern.Intersect(knownPattern[1]).Count() == intersectionsWith1 &&
                        pattern.Intersect(knownPattern[4]).Count() == intersectionsWith4);
                }

                knownPattern[0] = FindDigitPattern(6, 2, 3);
                knownPattern[2] = FindDigitPattern(5, 1, 2);
                knownPattern[3] = FindDigitPattern(5, 2, 3);
                knownPattern[5] = FindDigitPattern(5, 1, 3);
                knownPattern[6] = FindDigitPattern(6, 1, 3);
                knownPattern[9] = FindDigitPattern(6, 2, 4);

                int Decode(string digit) => Enumerable.Range(0, 10).Single(i => knownPattern[i].SetEquals(digit));

                result += output.Aggregate(0, (n, digit) => n * 10 + Decode(digit));
            }

            return result.ToString();
        }
    }
}