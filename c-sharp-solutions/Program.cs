using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Merry.Christmas
{
    internal class Program
    {
        /// <summary>
        ///     Runs the solution for the puzzle number passed in the command line arguments or prompts the user to input a puzzle
        ///     number/name to run
        /// </summary>
        private static void Main(string[] args)
        {
            var run = true;
            while (run)
            {
                if (args.Length == 0)
                {
                    // If no command line arguments are passed
                    // then prompt user for a puzzle number to run
                    Console.WriteLine(
                        "Input the puzzle number to execute (i.e. '1' or '1.2' or 'Day1' or 'Day1Part2') 'Q' to quit:");
                    var puzzleName = Console.ReadLine() ?? string.Empty;
                    args = puzzleName.Split(' ');
                }

                run = ProcessArgs(args.ToList());
                args = Array.Empty<string>();
            }
        }

        private static string InputFileName { get; set; } = "input.txt";

        private static bool ProcessArgs(IList<string> args)
        {
            if (args.Count == 0 || args.Contains("-help") || args.Contains("--help"))
            {
                Console.WriteLine("Usage:\n\tAoC2021 <puzzleName | puzzleNumber> [options]");
                Console.WriteLine("Options:\n\t-r\t\t\tAverage 100 iterations\n\t-i\t\t\tSet the input filename");
                Console.WriteLine(
                    "Examples:\n\tAoC2021 1\t\tinvokes Day1.Solve()\n\tAoC2021 Day1\t\tinvokes Day1.Solve()\n\tAoC2021 1.2\t\tinvokes Day1Part2.Solve()");
                Console.WriteLine("\tAoC2021 1*\t\tinvokes all Day1 solutions found in assembly");
                Console.WriteLine("\tAoC2021 1 -r\t\tinvokes Day1.Solve() 100 times and averages elapsed time");
                Console.WriteLine("\tAoC2021 1 -i test.txt\tinvokes Day1.Solve() with the 'test.txt' input file");
                Console.WriteLine();
                return true;
            }

            if (!InitializeInputFilename(args))
            {
                // Failed to initialize filename. Show help text.
                ProcessArgs(Array.Empty<string>());
                return true;
            }


            if (args[0].ToLowerInvariant() == "q")
            {
                return false;
            }

            var puzzleName = args[0];
            if (args.Contains("-r"))
            {
                RunAveragedPerformance(puzzleName);
            }
            else
            {
                RunPuzzleSolvers(puzzleName, false);
            }

            return true;
        }

        private static bool InitializeInputFilename(IList<string> args)
        {
            var inputIndex = args.IndexOf("-i");
            if (inputIndex > 0)
            {
                if (args.Count <= inputIndex + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input filename not supplied");
                    Console.ResetColor();
                    return false;
                }

                var filename = args[inputIndex + 1];
                char? quoteChar = null;
                if (filename.StartsWith("'"))
                {
                    quoteChar = '\'';
                }
                else if (filename.StartsWith('"'))
                {
                    quoteChar = '"';
                }

                if (quoteChar != null)
                {
                    var sb = new StringBuilder();
                    foreach (var value in args.Skip(inputIndex + 1))
                    {
                        if (value.EndsWith(quoteChar.Value))
                        {
                            sb.Append(value.TrimEnd(quoteChar.Value));
                        }
                        else
                        {
                            sb.Append(value + " ");
                        }
                    }

                    filename = sb.ToString().TrimStart(quoteChar.Value);
                }

                InputFileName = filename;
            }
            else
            {
                InputFileName = "input.txt";
            }

            return true;
        }

        /// <summary>
        /// Run the requested solvers over 100 iterations and average the elapsed time
        /// </summary>
        private static void RunAveragedPerformance(string puzzleName)
        {
            var solverTimes = new Dictionary<string, double>();
            var iterations = 0;
            while (iterations < 100)
            {
                iterations++;
                var results = RunPuzzleSolvers(puzzleName, true);
                if (results == null) return;
                foreach (var (solverName, solveTime) in results)
                    if (!solverTimes.ContainsKey(solverName)) solverTimes.Add(solverName, solveTime);
                    else solverTimes[solverName] += solveTime;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var (solverName, totalTime) in solverTimes)
            {
                Console.WriteLine($"{solverName} AVG:\t{totalTime / iterations:N3} ms.");
            }
            Console.ResetColor();
        }

        /// <summary>
        ///     Finds the puzzle solver type that matches the puzzle name and runs it.
        /// </summary>
        /// <remarks>
        ///     All of this is boilerplate code is just to make it easier to add C# classes that represent puzzle solutions.
        ///     As a "PuzzleSolution" designer, you just need to create a sub directory named "DayX" that contains the "input.txt"
        ///     Then you would also have a C# class defined in the assembly named "DayX.cs" that inherits "PuzzleSolver"
        /// </remarks>
        private static IReadOnlyList<(string solverName, double solveTime)> RunPuzzleSolvers(string puzzleName,
            bool silent)
        {
            // Use a convention to find the correct puzzle class type to instantiate and execute
            // This assumes that every C# puzzle class is going to be named "DayX" where X = the day number
            if (!puzzleName.StartsWith("Day")) puzzleName = $"Day{puzzleName}";
            var isWildcardSearch = false;
            if (puzzleName.Contains("*"))
            {
                isWildcardSearch = true;
                puzzleName = puzzleName.Replace("*", string.Empty);
            }

            var classTypeName = ConvertPuzzleNameToClassType(puzzleName);

            // Use reflection to find a class within this assembly that matches the puzzle name
            IList<Type> solverTypes;
            if (isWildcardSearch)
            {
                solverTypes = typeof(Program).Assembly.ExportedTypes.Where(t => t.Name.Contains(classTypeName))
                    .ToList();
            }
            else
            {
                var solverType = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(t => t.Name == classTypeName);
                solverTypes = solverType == null ? null : new[] { solverType };
            }

            if (solverTypes == null || solverTypes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Could not find a class named {classTypeName}");
                Console.ResetColor();
                return null;
            }

            var solveTimes = new List<(string, double)>();
            foreach (var solverType in solverTypes)
            {
                try
                {
                    var solveTime = RunSolve(puzzleName, solverType, silent);
                    solveTimes.Add((solverType.Name, solveTime));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e);
                    Console.ResetColor();
                }
            }

            return solveTimes;
        }


        private static string ConvertPuzzleNameToClassType(string puzzleName)
        {
            var classTypeName = puzzleName;
            if (classTypeName.Contains("."))
            {
                // Some puzzles have multiple parts
                // so if we want to run puzzle "1.2" we are going to assume
                // there is a C# class named "Day1Part2"
                classTypeName = classTypeName.EndsWith(".1")
                    ? classTypeName.Replace(".1", string.Empty)
                    : classTypeName.Replace(".", "Part");
            }

            if (classTypeName.Contains("-"))
            {
                // Some times there are multiple solvers for the same puzzle.
                // The commandline argument convention to run these additional solvers
                // will be to append "-X" where X represents the additional class name suffix
                classTypeName = classTypeName.Replace("-", string.Empty);
            }

            return classTypeName;
        }

        private static double RunSolve(string puzzleName, Type solverType, bool silent)
        {
            // Construct a new instance of the puzzle solver that matches the puzzle name/number
            var puzzleSolver = (PuzzleSolver)Activator.CreateInstance(solverType) ?? throw new NullReferenceException();

            var inputFilepath = GetInputFilepath(puzzleName, solverType, silent);

            var solveTime = puzzleSolver.Solve(inputFilepath, silent);
            return solveTime;
        }

        private static string GetInputFilepath(string puzzleName, MemberInfo solverType, bool silent)
        {
            // From the currently executing path, assume the input code is directly above the 'bin' directory
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var solutionsDirectory = currentPath.Split("bin\\")[0];

            var inputFilename = InputFileName;
            // Assume that the input.txt file exists in the directory matching the root puzzle name (i.e. Day1).
            // Remove any suffix (i.e. Part2 or -2 or .2) if it exists, because solvers for the same day use the same input
            var puzzleDirectory =
                Path.Combine(solutionsDirectory, puzzleName.Split("Part")[0].Split("-")[0].Split(".")[0]);
            var inputFilepath = Path.Combine(puzzleDirectory, inputFilename);
            if (!File.Exists(inputFilepath))
            {
                inputFilepath = Path.Combine(puzzleDirectory, "test.txt");
            }
            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    $"{solverType.Name} input: {Path.GetRelativePath(solutionsDirectory, inputFilepath)}");
                Console.ResetColor();
            }

            return inputFilepath;
        }
    }
}