using System;
using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day5
{
    public class Day5 : PuzzleSolver
    {
        public record Point(int X, int Y);
        
        public record Line(Point A, Point B)
        {
            public IEnumerable<Point> GetPoints()
            {
                return A.X == B.X
                    ? Enumerable.Range(Math.Min(A.Y, B.Y), Math.Abs(A.Y - B.Y) + 1).Select(y => new Point(A.X, y))
                    : A.Y == B.Y
                        ? Enumerable.Range(Math.Min(A.X, B.X), Math.Abs(A.X - B.X) + 1).Select(x => new Point(x, A.Y))
                        : Enumerable.Empty<Point>();
            }
        }

        protected override string Solve(string[] inputLines)
        {
            var lines = inputLines.Select(s => s.Split(" -> ")
                        .Select(n => n.Split(",")
                        .Select(int.Parse).ToArray())
                        .Select(p => new Point(p[0], p[1])).ToArray())
                        .Select(l => new Line(l[0], l[1]));
            var overlaps = new Dictionary<Point, int>();
            int overlapCount = 0;
            foreach (var line in lines)
            {
                foreach (var point in line.GetPoints())
                {
                    if (overlaps.ContainsKey(point))
                    {
                        overlaps[point]++;
                        if (overlaps[point] == 2) overlapCount++;
                    }
                    else
                    {
                        overlaps.Add(point, 1);
                    }
                }
            }

            return overlapCount.ToString();
        }
    }
}
