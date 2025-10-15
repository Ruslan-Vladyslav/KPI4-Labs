using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class InputHandler
    {
        private readonly FieldValidator _validator;
        private readonly string[] _args;

        public InputHandler(FieldValidator validator, string[] args)
        {
            _validator = validator;
            _args = args;
        }

        public (int generations, char[,] field, bool showInConsole) GetInput()
        {
            int generations = 0, rows = 0, cols = 0;
            char[,] field;
            string inputFile = string.Empty;
            int chosen = 0;

            if (_args.Length > 0)
            {
                inputFile = _args[0];
                chosen = 1;
            }
            else
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
            }

            if (chosen == 1)
            {
                if (_args.Length == 0)
                {
                    Console.Write("\nEnter file name (default input.txt): ");
                    inputFile = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(inputFile))
                    {
                        inputFile = "input.txt";
                    }
                }

                if (!File.Exists(inputFile))
                {
                    throw new FileNotFoundException($"Cannot find file '{inputFile}'.");
                }

                var lines = File.ReadAllLines(inputFile);
                generations = _validator.ValidateGenerations(lines[0].Trim());
                (rows, cols) = _validator.ValidateSize(lines[1].Trim());

                field = new char[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    string line = lines[i + 2];
                    _validator.ValidateLine(line, cols);

                    for (int j = 0; j < cols; j++)
                    {
                        field[i, j] = line[j];
                    }
                }
            }
            else
            {
                Console.Write("\nEnter number of generations: ");
                generations = _validator.ValidateGenerations(Console.ReadLine() ?? "0");

                Console.Write("Enter field size (cols rows): ");
                (rows, cols) = _validator.ValidateSize(Console.ReadLine() ?? "5 5");

                field = new char[rows, cols];

                Console.WriteLine("Enter field rows using '.' (dead) and 'x' (alive):");
                for (int i = 0; i < rows; i++)
                {
                    string line = Console.ReadLine() ?? "";
                    _validator.ValidateLine(line, cols);

                    for (int j = 0; j < cols; j++)
                    {
                        field[i, j] = line[j];
                    }
                }
            }

            bool showGenerations = _validator.ValidateInt("Display all generations in console? [1 - yes | 2 - no]: ");

            return (generations, field, showGenerations);
        }
    }
}
