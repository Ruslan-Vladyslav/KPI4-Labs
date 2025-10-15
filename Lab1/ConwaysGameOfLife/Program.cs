using System;
using System.IO;
using System.Threading;

namespace ConwaysGameOfLife
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var validator = new FieldValidator();
                var handler = new InputHandler(validator, args);

                var (generations, field, showInConsole) = handler.GetInput();

                var game = new GameOfLife(generations, field);
                game.Run(showInConsole);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
