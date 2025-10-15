using ConwaysGameOfLife;

namespace GameOfLifeTests
{
    public class GameOfLifeTest
    {
        [Fact]
        public void SingleCellDies()
        {
            char[,] field = {
                {'.', '.', '.'},
                {'.', 'x', '.'},
                {'.', '.', '.'}
            };

            var game = new GameOfLife(1, field);
            var result = game.AdvanceGenerations();

            Assert.Equal('.', result[1, 1]);
        }

        [Fact]
        public void TwoCellsDie_Underpopulation()
        {
            char[,] field = {
                {'.', 'x', '.'},
                {'.', 'x', '.'},
                {'.', '.', '.'}
            };

            var game = new GameOfLife(1, field);
            var result = game.AdvanceGenerations();

            Assert.Equal('.', result[0, 1]);
            Assert.Equal('.', result[1, 1]);
        }

        [Fact]
        public void ThreeCellsForm()
        {
            char[,] field = {
                {'.', '.', '.'},
                {'x', 'x', 'x'},
                {'.', '.', '.'}
            };

            var game = new GameOfLife(1, field);
            var result = game.AdvanceGenerations();

            Assert.Equal('x', result[0, 1]);
            Assert.Equal('x', result[1, 1]);
            Assert.Equal('x', result[2, 1]);
        }

        [Fact]
        public void BlockPattern()
        {
            char[,] field = {
                {'.', '.', '.', '.'},
                {'.', 'x', 'x', '.'},
                {'.', 'x', 'x', '.'},
                {'.', '.', '.', '.'}
            };

            var game = new GameOfLife(1, field);
            var result = game.AdvanceGenerations();

            Assert.Equal('x', result[1, 1]);
            Assert.Equal('x', result[1, 2]);
            Assert.Equal('x', result[2, 1]);
            Assert.Equal('x', result[2, 2]);
        }

        [Fact]
        public void GliderMovesCorrectly()
        {
            char[,] field = {
        {'.', '.', '.', '.', '.'},
        {'.', '.', 'x', '.', '.'},
        {'.', '.', '.', 'x', '.'},
        {'.', 'x', 'x', 'x', '.'},
        {'.', '.', '.', '.', '.'}
    };

            var game = new GameOfLife(1, field);
            var result = game.AdvanceGenerations();

            Assert.Equal('x', result[2, 1]);
        }

        [Fact]
        public void SaveToFile()
        {
            char[,] field = {
                {'.', 'x', '.'},
                {'.', 'x', '.'},
                {'.', 'x', '.'}
            };

            var game = new GameOfLife(1, field);
            string tempFile = Path.GetTempFileName();

            game.Run(showInConsole: false, outputFile: tempFile);

            string[] lines = File.ReadAllLines(tempFile);

            Assert.Equal("xxx", lines[0]);
            Assert.Equal("xxx", lines[1]);
            Assert.Equal("xxx", lines[2]);

            File.Delete(tempFile);
        }
    }
}
