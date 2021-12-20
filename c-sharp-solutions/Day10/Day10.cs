using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day10
{
    public class Day10 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines) => inputLines.Sum(line => GetSyntaxErrorScore(line, new Stack<char>())).ToString();

        protected static int GetSyntaxErrorScore(string line, Stack<char> stack)
        {
            foreach (var ch in line)
                switch (stack.FirstOrDefault(), ch)
                {
                    case ('(', ')'):
                    case ('[', ']'):
                    case ('{', '}'):
                    case ('<', '>'):
                        stack.Pop();
                        break;
                    case (_, ')'):
                        return 3;
                    case (_, ']'):
                        return 57;
                    case (_, '}'):
                        return 1197;
                    case (_, '>'):
                        return 25137;
                    default:
                        stack.Push(ch);
                        break;
                }

            return 0;
        }
    }
}