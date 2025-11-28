using TetrisApp.Services.Interfaces;

namespace TetrisApp.IO
{
    public class ConsoleErrorReporter : IErrorReporter
    {
        public void ReportError()
        {
            Console.WriteLine("Error: Invalid input data.");
        }
    }
}
