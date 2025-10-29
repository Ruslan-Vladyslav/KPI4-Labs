using System;
using System.IO;

namespace Calculator
{
    public static class IOManager
    {
        public static string GetInput(string[] args)
        {
            int chosen = 0;
            string inputFile = string.Empty;

            if (args.Length > 0 && File.Exists(args[0]))
            {
                string input = File.ReadAllText(args[0]);
                Console.WriteLine($"\nReading input from file: {args[0]}");
                Console.WriteLine($"Input: {input}");
                return input;
            } else
            {
                Console.WriteLine("Choose input:");
                Console.WriteLine("1. Data in .txt file");
                Console.WriteLine("2. Input manually");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out chosen) && (chosen == 1 || chosen == 2))
                    {
                        break;
                    }

                    Console.Write("Invalid choice. Enter 1 or 2: ");
                }

                if (chosen == 1)
                {
                    Console.Write("\nEnter file name (default input.txt): ");
                    inputFile = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputFile))
                    {
                        inputFile = "input.txt";
                    }

                    if (!File.Exists(inputFile))
                    {
                        Console.WriteLine($"\nCannot find file '{inputFile}'\n");
                        return GetInput(Array.Empty<string>());
                    }

                    string input = File.ReadAllText(inputFile);
                    Console.WriteLine($"\nReading input from file: {inputFile}");
                    Console.WriteLine($"Input: {input}");
                    return input;
                } else
                {

                    Console.Write("\nEnter your calculation: ");
                    return Console.ReadLine();
                }
            }
        }

        public static void HandleOutput(string[] args, int result)
        {
            string outputFile = "output.txt";
            File.WriteAllText(outputFile, result.ToString());
            Console.WriteLine($"Result saved to file: {outputFile}");
        }
    }
}
