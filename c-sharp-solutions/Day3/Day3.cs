namespace Merry.Christmas
{
    public class Day3 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < inputLines[0].Length; i++)
            {
                int totalSetBits = 0;
                foreach (var line in inputLines)
                {
                    if (line[i] != '0') totalSetBits++;
                }

                gamma <<= 1;
                epsilon <<= 1;

                if (totalSetBits >= inputLines.Length / 2)
                {
                    gamma += 1;
                }
                else
                {
                    epsilon += 1;
                }
            }

            return $"{gamma * epsilon}";
        }
    }
}