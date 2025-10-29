using System;
using System.IO;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var validator = new Validator();
            var handler = new CalculateHandler(validator);
            string enter;

            do
            {
                string input = IOManager.GetInput(args);
                string[] keys = handler.Parse(input);

                var validation = validator.ValidateInput(keys);

                if (!validation.IsValid)
                {
                    Console.WriteLine($"\nError: {validation.Message}");
                }
                else
                {
                    int result = handler.Calculate(keys);

                    Console.WriteLine($"\nResult: {result}");
                    IOManager.HandleOutput(args, result);
                }

                Console.Write("\nContinue program (y - yes | n - no): ");
                enter = Console.ReadLine();
                Console.Write("\n");

            } while (enter == "y" || enter == "Y");

            Console.WriteLine("\nExiting program");
        }
    }
}
