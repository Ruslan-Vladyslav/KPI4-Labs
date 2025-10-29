using System;

namespace Calculator
{
    public class CalculateHandler
    {
        private readonly Validator _validator;

        public CalculateHandler(Validator validator)
        {
            this._validator = validator;
        }

        public string[] Parse(string input)
        {
            return input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        public int Calculate(string[] keys)
        {
            var state = new CalculatorState();

            foreach (string key in keys)
            {
                HandleKeyPress(state, key);
            }

            return state.Screen;
        }

        private void HandleKeyPress(CalculatorState state, string key)
        {
            if (int.TryParse(key, out int digit))
            {
                if (state.StartSecondNumber)
                {
                    state.Screen = digit;
                    state.StartSecondNumber = false;
                }
                else
                {
                    state.Screen = state.Screen * 10 + digit;
                }
            }
            else if (_validator.IsOperator(key))
            {
                state.Op = key;
                state.FirstNumber = state.Screen;
                state.StartSecondNumber = true;
            }
            else if (key == "=")
            {
                switch (state.Op)
                {
                    case "+": state.Screen = state.FirstNumber + state.Screen; break;
                    case "-": state.Screen = state.FirstNumber - state.Screen; break;
                    case "*": state.Screen = state.FirstNumber * state.Screen; break;
                    case "/": state.Screen = state.Screen == 0 ? 0 : state.FirstNumber / state.Screen; break;
                }

                state.Op = string.Empty;
                state.StartSecondNumber = false;
            }
        }
    }
}
