using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisApp.IO
{
    public class FileReader : IScreenReader
    {
        public GameScreen ReadScreen(string filePath)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                throw new InvalidDataException("Error reading file access or path.", ex);
            }

            if (lines.Length < 1)
                throw new InvalidDataException("File is empty.");

            string[] dimensions = lines[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (dimensions.Length != 2 ||
                !int.TryParse(dimensions[0], out int height) ||
                !int.TryParse(dimensions[1], out int width) ||
                height <= 0 || width <= 0)
            {
                throw new InvalidDataException("Invalid screen dimensions in the first line.");
            }

            if (lines.Length != height + 1)
            {
                throw new InvalidDataException($"Expected {height} data rows, found {lines.Length - 1}.");
            }

            var piecePoints = new List<Point>();
            var landscapePoints = new List<Point>();
            int pieceCount = 0;

            for (int y = 0; y < height; y++)
            {
                string row = lines[y + 1];
                if (row.Length != width) throw new InvalidDataException($"Row {y + 1} has incorrect length.");

                for (int x = 0; x < width; x++)
                {
                    char c = row[x];
                    if (c == 'p')
                    {
                        piecePoints.Add(new Point(x, y));
                        pieceCount++;
                    }
                    else if (c == '#')
                    {
                        landscapePoints.Add(new Point(x, y));
                    }
                    else if (c != '.')
                    {
                        throw new InvalidDataException($"Unknown character '{c}' at ({x}, {y}).");
                    }
                }
            }

            if (pieceCount == 0) 
                throw new InvalidDataException("The input must contain exactly one piece ('p').");

            return new GameScreen(width, height, piecePoints, landscapePoints);
        }
    }
}
