
namespace ConwaysGameOfLife
{
    public class FieldValidator
    {
        public void ValidateLine(string line, int expectedLength)
        {
            if (line.Length != expectedLength)
            {
                throw new ArgumentException($"Each line must have exactly {expectedLength} characters.");
            }
            foreach (char c in line)
            {
                if (c != '.' && c != 'x')
                {
                    throw new ArgumentException("Field can only contain '.' or 'x'.");
                }
            }
        }

        public (int rows, int cols) ValidateSize(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int cols) ||
                !int.TryParse(parts[1], out int rows))
            {
                throw new ArgumentException("Invalid size format. Use: cols rows");
            }

            return (rows, cols);
        }

        public int ValidateGenerations(string input)
        {
            if (!int.TryParse(input, out int generations))
            {
                throw new ArgumentException("Invalid number of generations");
            }

            return generations;
        }

        public bool ValidateInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (input == "1") return true;
                if (input == "2") return false;

                Console.WriteLine("Invalid input. Enter 1 (yes) or 2 (no).");
            }
        }
    }
}
