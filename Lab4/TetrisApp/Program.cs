using TetrisApp.Core;
using TetrisApp.IO;
using TetrisApp.Services;
using TetrisApp.Services.Interfaces;

namespace TetrisApp
{
    class Program
    {
        private const string DefaultFileName = "input.txt";
        private const string AllSteps = "--steps";

        static void Main(string[] args)
        {
            string filePath;
            bool printAllSteps = false;

            if (args.Length > 0)
            {
                filePath = args[0];

                if (args.Length > 1 && args[1].Equals(AllSteps, StringComparison.OrdinalIgnoreCase))
                {
                    printAllSteps = true;
                }
            }
            else
            {
                Console.WriteLine("File path not provided.");
                Console.Write($"Press Enter to use default file ({DefaultFileName}), or type a file path: ");

                string? filePathInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(filePathInput))
                {
                    filePath = DefaultFileName;
                }
                else
                {
                    filePath = filePathInput;
                }

                Console.Write("Do you want to print all steps? (Y/N): ");
                string? modeInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(modeInput) &&
                    (modeInput.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                     modeInput.Equals("Yes", StringComparison.OrdinalIgnoreCase)))
                {
                    printAllSteps = true;
                }
                else
                {
                    printAllSteps = false;
                }
            }

            IScreenReader reader = new FileReader();
            IScreenWriter writer = new ConsoleWriter();
            IErrorReporter reporter = new ConsoleErrorReporter();
            IGame engine = new Game();

            TetrisGameService gameService = new TetrisGameService(reader, writer, reporter, engine);
            gameService.Run(filePath, printAllSteps);
        }
    }
}