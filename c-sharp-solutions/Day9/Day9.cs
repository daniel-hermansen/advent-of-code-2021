using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day9
{
    public class Day9 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var floorMap = new FloorLocation[inputLines.Length, inputLines[0].Length];
            for (var y = 0; y < inputLines.Length; y++)
                for (var x = 0; x < inputLines[y].Length; x++)
                {
                    floorMap[y, x] = CreateFloorLocation(x, y, inputLines[y][x] - '0', floorMap);
                }
            return Solve(floorMap);
        }

        protected virtual FloorLocation CreateFloorLocation(int x, int y, int height, FloorLocation[,] floorMap) => new(x, y, height, floorMap);

        protected virtual string Solve(FloorLocation[,] floorMap)
        {
            return floorMap.Cast<FloorLocation>()
                .Where(position => position.IsLowPoint)
                .Select(position => 1 + position.Height).Sum().ToString();
        }

        public record FloorLocation(int X, int Y, int Height, FloorLocation[,] Map)
        {
            public IEnumerable<FloorLocation> Adjacent => new[] { Left, Top, Right, Bottom }.Where(t => t != null);

            public bool IsLowPoint => Adjacent.All(v => Height < v.Height);
            public bool HasLeft => X > 0;
            public bool HasTop => Y > 0;
            public bool HasRight = X < Map.GetLength(1) - 1;
            public bool HasBottom => Y < Map.GetLength(0) - 1;

            public FloorLocation Left => HasLeft ? Map[Y, X - 1] : null;
            public FloorLocation Top => HasTop ? Map[Y - 1, X] : null;
            public FloorLocation Right => HasRight ? Map[Y, X + 1] : null;
            public FloorLocation Bottom => HasBottom ? Map[Y + 1, X] : null;
        }
    }
}