using System;
using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day5
{
    public class Day5 : PuzzleSolver
    {
        protected bool ExcludeDiagonal { get; set; } = true;

        public static IEnumerable<Line> ExtractLines(IEnumerable<string> inputLines) =>
            inputLines.Select(s => s.Split(" -> ")
                      .Select(n => n.Split(",")
                      .Select(int.Parse).ToArray())
                      .Select(p => new Point(p[0], p[1])).ToArray())
                      .Select(l => new Line(l[0], l[1]));

        protected override string Solve(string[] inputLines)
        {
            var lines = ExtractLines(inputLines);
            var overlaps = new Dictionary<Point, int>();
            var overlapCount = 0;
            foreach (var line in lines)
                foreach (var point in line.GetPoints(ExcludeDiagonal))
                    if (overlaps.ContainsKey(point))
                    {
                        overlaps[point]++;
                        if (overlaps[point] == 2) overlapCount++;
                    }
                    else
                    {
                        overlaps.Add(point, 1);
                    }

            return overlapCount.ToString();
        }

        public record Point(int X, int Y);

        public record Line(Point A, Point B)
        {
            public IEnumerable<Point> GetPoints(bool excludeDiagonal = true)
            {
                return A.X == B.X
                    ? RangeY().Select(y => new Point(A.X, y))
                    : A.Y == B.Y
                        ? RangeX().Select(x => new Point(x, A.Y))
                        : excludeDiagonal
                            ? Enumerable.Empty<Point>()
                            : RangeX().Zip(A.X < B.X && A.Y > B.Y || A.Y < B.Y && A.X > B.X
                                ? RangeY().Reverse()
                                : RangeY()).Select(p => new Point(p.First, p.Second));
            }

            private IEnumerable<int> RangeX() => Enumerable.Range(Math.Min(A.X, B.X), Math.Abs(A.X - B.X) + 1);

            private IEnumerable<int> RangeY() => Enumerable.Range(Math.Min(A.Y, B.Y), Math.Abs(A.Y - B.Y) + 1);
        }
    }
}