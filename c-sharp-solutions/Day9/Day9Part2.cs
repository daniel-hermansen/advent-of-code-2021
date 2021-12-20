using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day9
{
    public class Day9Part2 : Day9
    {
        protected override string Solve(FloorLocation[,] floorMap)
        {
            return floorMap.Cast<BasinLocation>()
                .Where(position => position.IsLowPoint)
                .Select(position => position.Basin.Count)
                .OrderByDescending(size => size)
                .Take(3)
                .Aggregate(1, (a, b) => a * b)
                .ToString();
        }

        protected override FloorLocation CreateFloorLocation(int x, int y, int height, FloorLocation[,] floorMap) => new BasinLocation(x, y, height, floorMap);

        public record BasinLocation(int X, int Y, int Height, FloorLocation[,] Map) : FloorLocation(X, Y, Height, Map)
        {
            private HashSet<BasinLocation> _basin;
            public ICollection<BasinLocation> Basin => _basin ?? ExploreBasin();

            private ICollection<BasinLocation> ExploreBasin(HashSet<BasinLocation> basin = null)
            {
                _basin = basin ??= new HashSet<BasinLocation>();
                basin.Add(this);
                foreach (var neighbor in Adjacent.Cast<BasinLocation>())
                    if (neighbor.Height < 9 && !basin.Contains(neighbor))
                        neighbor.ExploreBasin(basin);
                return basin;
            }
        }
    }
}