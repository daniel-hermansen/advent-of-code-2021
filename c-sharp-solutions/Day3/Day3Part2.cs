using System;
using System.Linq;

namespace Merry.Christmas
{
    public class Day3Part2 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var oxygenRating = RunBitCriteriaFilter(inputLines, '1', '0');
            var co2Rating = RunBitCriteriaFilter(inputLines, '0', '1');
            return (Convert.ToInt32(oxygenRating[0], 2) * Convert.ToInt32(co2Rating[0], 2)).ToString();
        }

        private static string[] RunBitCriteriaFilter(string[] bitField, char tiebreaker, char alternate)
        {
            for (var i = 0; i < bitField[0].Length && bitField.Length > 1; i++)
            {
                int totalSetBits = 0;
                foreach (var line in bitField)
                {
                    if (line[i] != '0') totalSetBits++;
                }

                var requiredBit = totalSetBits * 2 >= bitField.Length ? tiebreaker : alternate;
                bitField = bitField.Where(s => s[i] == requiredBit).ToArray();
            }

            return bitField;
        }
    }
}