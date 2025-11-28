using TetrisApp.Core;
using TetrisApp.IO;
using TetrisApp.Services;
using TetrisApp.Services.Interfaces;
using System;
using System.IO;

namespace TetrisApp
{
    class Program
    {
        private const string DefaultFileName = "input.txt";

        static void Main(string[] args)
        {
            string filePath;

            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                Console.WriteLine("File path not provided.");
                Console.Write($"Press Enter to use default file ({DefaultFileName}), or type a file path: ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    filePath = DefaultFileName;
                }
                else
                {
                    filePath = input;
                }
            }

            IScreenReader reader = new FileReader();
            IScreenWriter writer = new ConsoleWriter();
            IErrorReporter reporter = new ConsoleErrorReporter();
            IGame engine = new Game();

            TetrisGameService gameService = new TetrisGameService(reader, writer, reporter, engine);
            gameService.Run(filePath);
        }
    }
}