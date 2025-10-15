using System;
using System.IO;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        private readonly int _generations;
        private readonly char[,] _field;

        public GameOfLife(int generations, char[,] field)
        {
            _generations = generations;
            _field = (char[,])field.Clone(); 
        }

        public char[,] Run(bool showInConsole = true, int delayMs = 500, TextWriter writer = null!, string? outputFile = null)
        {
            writer ??= Console.Out;

            writer.WriteLine("Generation 0:");
            DisplayField(_field, writer);

            if (showInConsole)
            {
                DisplayGenerations(delayMs, writer);
            }

            char[,] result = AdvanceGenerations();

            if (!string.IsNullOrEmpty(outputFile))
            {
                SaveToFile(result, outputFile);
            }

            return result;
        }

        public char[,] AdvanceGenerations()
        {
            char[,] current = (char[,])_field.Clone();
            for (int i = 0; i < _generations; i++)
            {
                current = NextGeneration(current);
            }
            return current;
        }

        public void DisplayGenerations(int delayMs = 500, TextWriter writer = null!)
        {
            writer ??= Console.Out;
            Console.CursorVisible = false;

            char[,] current = (char[,])_field.Clone();

            for (int g = 0; g < _generations; g++)
            {
                current = NextGeneration(current);
                writer.WriteLine($"Generation: {g + 1}/{_generations}");
                DisplayField(current, writer);
                Thread.Sleep(delayMs);
            }

            Console.CursorVisible = true;
        }

        private static void DisplayField(char[,] field, TextWriter writer)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    writer.Write(field[i, j]);
                writer.WriteLine();
            }
        }

        private static char[,] NextGeneration(char[,] current) 
        { 
            int rows = current.GetLength(0); 
            int cols = current.GetLength(1); 

            char[,] next = new char[rows, cols]; 

            for (int i = 0; i < rows; i++)
            { 
                for (int j = 0; j < cols; j++)
                { 
                    int neighbors = CountAliveNeighbors(current, i, j); 
                    bool isAlive = current[i, j] == 'x'; 

                    if (isAlive) next[i, j] = (neighbors == 2 || neighbors == 3) ? 'x' : '.'; 
                    else next[i, j] = (neighbors == 3) ? 'x' : '.'; 
                } 
            } 
            return next;
        }

        private static int CountAliveNeighbors(char[,] field, int x, int y)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);
            int count = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = (x + dx + rows) % rows;
                    int ny = (y + dy + cols) % cols;

                    if (field[nx, ny] == 'x')
                        count++;
                }
            }

            return count;
        }

        private static void SaveToFile(char[,] field, string outputFile)
        {
            using StreamWriter sw = new StreamWriter(outputFile);
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    sw.Write(field[i, j]);
                sw.WriteLine();
            }
        }
    }
}
