namespace Calculator
{
    public class CalculatorState
    {
        public int Screen { get; set; } = 0;
        public int FirstNumber { get; set; } = 0;
        public string Op { get; set; } = string.Empty;
        public bool StartSecondNumber { get; set; } = false;
    }
}
