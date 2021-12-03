using System;
using System.IO;
using System.Linq;

namespace Merry.Christmas
{
    class Program
    {
        /// <summary>
        /// Runs the solution for the puzzle number passed in the command line arguments or prompts the user to input a puzzle number/name to run
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // If no command line arguments are passed
                // then prompt user for a puzzle number to run
                Console.Clear();
                Console.WriteLine("Input the puzzle number to execute (i.e. '1' or '1.2' or 'Day1' or 'Day1Part2'):");
                var input = Console.ReadLine();
                RunPuzzleSolution(input);
                Console.WriteLine();
                Console.WriteLine($"ENTER to start over. 'R' or '{input[0]}' to repeat. Any other key to quit...");
                var key = Console.ReadKey();
                double totalTime = 0;
                int iterations = 0;
                while (key.KeyChar == input[0] || key.Key == ConsoleKey.R)
                {
                    // If they hit the same 1st key, run the same puzzle solver
                    Console.WriteLine($" -> {input}");
                    iterations++;
                    totalTime += RunPuzzleSolution(input);
                    Console.WriteLine($"AVG: {totalTime / iterations:N3} ms");
                    key = Console.ReadKey();
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    Main(args);
                }
            }
            else
            {
                // Otherwise assume the 1st argument is the puzzle name or number
                RunPuzzleSolution(args[0]);
            }
        }

        /// <summary>
        /// Finds the puzzle solver type that matches the puzzle name and runs it.
        /// </summary>
        /// <remarks>
        /// All of this is boilerplate code is just to make it easier to add C# classes that represent puzzle solutions.
        /// As a "PuzzleSolution" designer, you just need to create a sub directory named "DayX" that contains the "input.txt"
        /// Then you would also have a C# class defined in the assembly named "DayX.cs" that inherits "PuzzleSolver"
        /// </remarks>
        /// <param name="puzzleName"></param>
        private static double RunPuzzleSolution(string puzzleName)
        {
            // Use a convention to find the correct puzzle class to execute
            // This assumes that every C# puzzle class is going to be named "DayX" where X = the day number
            if (!puzzleName.Contains("Day"))
            {
                puzzleName = $"Day{puzzleName}";
            }
            string classTypeName = puzzleName;
            if (classTypeName.Contains("."))
            {
                // Some puzzles have multiple parts
                // so if we want to run puzzle "1.2" we are going to assume
                // there is a C# class named "Day1Part2"
                classTypeName = classTypeName.Replace(".","Part");
            }

            if (classTypeName.Contains("-"))
            {
                classTypeName = classTypeName.Replace("-", string.Empty);
            }
            // Use reflection to find a class within this assembly that matches the puzzle name
            var solverType = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(t => t.Name == classTypeName);
            if (solverType == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Could not find a class named {classTypeName}");
                return -1;
            }
            // Construct a new instance of the puzzle solver that matches the puzzle name/number
            var puzzleSolver = (PuzzleSolver)Activator.CreateInstance(solverType);

            // From the currently executing path, assume the input code is directly above the 'bin' directory
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var solutionsDirectory = currentPath.Split("bin\\")[0];

            // Assume that the input.txt file exists in the directory matching the root puzzle name (i.e. Day1).
            // Remove any suffix (i.e. Part2 or -2 or .2) if it exists, because solvers for the same day use the same input
            var inputFilepath = Path.Combine(solutionsDirectory,
                puzzleName.Split("Part")[0].Split("-")[0].Split(".")[0], "input.txt");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{classTypeName} input: {Path.GetRelativePath(solutionsDirectory, inputFilepath)}");
            Console.ResetColor();
            var result = puzzleSolver.Solve(inputFilepath);
            return result;
        }
    }
}
