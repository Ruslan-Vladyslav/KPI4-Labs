using ConwaysGameOfLife;

namespace GameOfLifeTests
{
    public class FieldValidatorTest
    {
        [Fact]
        public void ValidateLine_WrongLength()
        {
            var validator = new FieldValidator();
            string line = "x..";

            var ex = Assert.Throws<ArgumentException>(() => validator.ValidateLine(line, 4));
            Assert.Contains("Each line must have exactly", ex.Message);
        }

        [Fact]
        public void ValidateLine_InvalidCharacter()
        {
            var validator = new FieldValidator();
            string line = "x.o.";

            var ex = Assert.Throws<ArgumentException>(() => validator.ValidateLine(line, 4));
            Assert.Contains("Field can only contain", ex.Message);
        }

        [Fact]
        public void ValidateSize_ReturnsRowsCols()
        {
            var validator = new FieldValidator();
            string input = "5 4";

            var (rows, cols) = validator.ValidateSize(input);

            Assert.Equal(4, rows);
            Assert.Equal(5, cols);
        }

        [Fact]
        public void ValidateSize_InvalidInput()
        {
            var validator = new FieldValidator();
            string input = "5x4";

            Assert.Throws<ArgumentException>(() => validator.ValidateSize(input));
        }

        [Fact]
        public void ValidateGenerations_ReturnsInt()
        {
            var validator = new FieldValidator();
            string input = "10";

            int generations = validator.ValidateGenerations(input);

            Assert.Equal(10, generations);
        }

        [Fact]
        public void ValidateGenerations_InvalidNumber()
        {
            var validator = new FieldValidator();
            string input = "abc";

            Assert.Throws<ArgumentException>(() => validator.ValidateGenerations(input));
        }
    }
}
