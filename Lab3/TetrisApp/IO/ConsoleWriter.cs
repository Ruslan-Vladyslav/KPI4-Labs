using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisApp.IO
{
    public class ConsoleWriter : IScreenWriter
    {
        public void WriteScreen(GameScreen finalScreen)
        {
            char[,] grid = new char[finalScreen.Height, finalScreen.Width];

            for (int i = 0; i < finalScreen.Height; i++)
            {
                for (int j = 0; j < finalScreen.Width; j++)
                {
                    grid[i, j] = '.';
                }
            }

            foreach (var p in finalScreen.Landscape)
            {
                if (p.Y >= 0 && p.Y < finalScreen.Height && p.X >= 0 && p.X < finalScreen.Width)
                {
                    grid[p.Y, p.X] = '#';
                }
            }

            foreach (var p in finalScreen.Piece)
            {
                if (p.Y >= 0 && p.Y < finalScreen.Height && p.X >= 0 && p.X < finalScreen.Width)
                {
                    grid[p.Y, p.X] = 'p';
                }
            }

            for (int i = 0; i < finalScreen.Height; i++)
            {
                for (int j = 0; j < finalScreen.Width; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
