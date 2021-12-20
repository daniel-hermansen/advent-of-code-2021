using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day10
{
    public class Day10Part2 : Day10
    {
        protected override string Solve(string[] inputLines)
        {
            var scores = inputLines.Select(GetAutocompleteScore).Where(s => s > 0).OrderBy(v => v).ToArray();
            return scores[scores.Length / 2].ToString();
        }

        protected static long GetAutocompleteScore(string line)
        {
            var stack = new Stack<char>();
            var score = GetSyntaxErrorScore(line, stack);
            return score == 0 ? stack.Aggregate(0L, (s, c) => s * 5 + ToPointValue(c)) : 0;
        }

        private static int ToPointValue(char c) => c switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4,
            _ => 0
        };
    }
}