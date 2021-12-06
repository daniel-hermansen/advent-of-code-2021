using System.Linq;

namespace Merry.Christmas
{
    public class Day1OneLiner : PuzzleSolver
    {
        protected override string Solve(string[] input)
        {
            return input.Zip(input.Skip(1)).Sum(d => int.Parse(d.First) < int.Parse(d.Second) ? 1 : 0).ToString();
        }
    }

    public class Day1Part2OneLiner : PuzzleSolver
    {
        protected override string Solve(string[] input)
        {
            var depths = input.ToIntArray(); return depths.Zip(depths.Skip(3)).Sum(d => d.First < d.Second ? 1 : 0).ToString();
        }
    }
}