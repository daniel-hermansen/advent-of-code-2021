using System;
using System.IO;
using System.Linq;

namespace Merry.Christmas
{
    class Program
    {
        // All of this is boilerplate code is just to make it easier to add C# classes that represent puzzle solutions
        // As a "PuzzleSolution" designer, you just need to create a sub directory named "DayX" that contains the "input.txt"
        // Then you would also have a C# class defined in the assembly named "DayX.cs" that inherits "PuzzleSolver"

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
            }
            else
            {
                // Otherwise assume the 1st argument is the puzzle name or number
                RunPuzzleSolution(args[0]);
            }
        }

        private static void RunPuzzleSolution(string puzzleName)
        {
            // Use a convention to find the correct puzzle class to execute
            // We're going to assume that every C# puzzle class is going to be named "DayX" where X = the day number
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var solutionsDirectory = currentPath.Split("bin\\")[0];

            if (puzzleName.Contains("."))
            {
                // Some puzzles have multiple parts
                // so if we want to run puzzle "1.2" we are going to assume
                // there is a C# class named "Day1Part2"
                puzzleName = puzzleName.Replace(".","Part");
            }
            if (!puzzleName.Contains("Day"))
            {
                puzzleName = $"Day{puzzleName}";
            }
            // Use reflection to find a class within this assembly that matches the puzzle name
            var solutionType = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(t => t.Name == puzzleName);
            if (solutionType == null)
            {
                Console.WriteLine($"Could not find a class named {puzzleName}");
                return;
            }
            // Construct a new intance of the puzzle solver that matches the puzzle name/number
            var puzzleSolver = (PuzzleSolver)Activator.CreateInstance(solutionType);

            // Assume that the input.txt file exists in the directory matching the puzzle name (without the optional 'PartX')
            puzzleSolver.Run(Path.Combine(solutionsDirectory, puzzleName.Split("Part")[0], "input.txt"));
        }
    }
}
