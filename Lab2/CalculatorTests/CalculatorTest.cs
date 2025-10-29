using Calculator;

namespace CalculatorTests
{
    public class CalculatorTest
    {
        private readonly Calculator.Validator _validator;
        private readonly CalculateHandler _handler;

        public CalculatorTest()
        {
            _validator = new Calculator.Validator();
            _handler = new CalculateHandler(_validator);
        }


        [Theory]
        [InlineData("2 + 3 =", 5)]
        [InlineData("10 - 4 =", 6)]
        [InlineData("6 * 7 =", 42)]
        [InlineData("8 / 2 =", 4)]
        [InlineData("15 / 0 =", 0)]

        [InlineData("9 + 1 =", 10)]
        [InlineData("10 - 20 =", -10)]
        [InlineData("7 * 6 =", 42)]
        [InlineData("100 / 4 =", 25)]
        public void ValidOperationCalculate(string input, int expected)
        {
            string[] keys = _handler.Parse(input);
            int result = _handler.Calculate(keys);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("+ 2 3 =", false)]
        [InlineData("2 + + 3 =", false)]
        [InlineData("2 + =", false)]
        [InlineData("= 2 + 3", false)]
        [InlineData("2 =", false)]
        [InlineData("/ =", false)]
        [InlineData("2 3 5 =", false)]
        public void InvalidInput_FailValidation(string input, bool expectedValid)
        {
            string[] keys = _handler.Parse(input);
            var validationResult = _validator.ValidateInput(keys);

            bool isValid = validationResult.IsValid;

            Assert.Equal(expectedValid, isValid);
        }

        [Fact]
        public void ParseReturnsCorrectKeys()
        {

            string[] keys = _handler.Parse(" 1  2 +   3 = ");

            Assert.Equal(new[] { "1", "2", "+", "3", "=" }, keys);
        }
    }
}
