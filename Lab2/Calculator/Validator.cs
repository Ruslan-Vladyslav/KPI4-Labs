using System;
using System.Linq;

namespace Calculator
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class Validator
    {
        private readonly string _validKeys;
        
        public Validator()
        {
            this._validKeys = "0123456789+-*/=";
        }

        public ValidationResult ValidateInput(string[] keys)
        {
            var result = new ValidationResult();

            if (keys.Any(k => k.Length > 1 && !int.TryParse(k, out _)))
            {
                result.IsValid = false;
                result.Message = $"Incorrect token: \"{keys.First(k => k.Length > 1 && !int.TryParse(k, out _))}\"";
                return result;
            }

            if (keys.Any(k => k.Length == 1 && !_validKeys.Contains(k) && !int.TryParse(k, out _)))
            {
                result.IsValid = false;
                result.Message = $"Unrecognizable symbol: \"{keys.First(k => k.Length == 1 && !_validKeys.Contains(k))}\"";
                return result;
            }

            if (IsOperator(keys[0]) || keys[0] == "=")
            {
                result.IsValid = false;
                result.Message = "Expression cannot start with an operator or '='";
                return result;
            }

            int opCount = keys.Count(k => IsOperator(k));
            if (opCount > 1)
            {
                result.IsValid = false;
                result.Message = "Only one arithmetic operation is allowed";
                return result;
            }

            int eqIndex = Array.IndexOf(keys, "=");
            if (eqIndex != -1 && eqIndex != keys.Length - 1)
            {
                result.IsValid = false;
                result.Message = "The '=' sign must be the last key";
                return result;
            }

            if (!keys.Any(IsOperator) && keys.Contains("="))
            {
                result.IsValid = false;
                result.Message = "Expression must contain an operator before '='";
                return result;
            }
            int opIndex = Array.FindIndex(keys, IsOperator);
            if (opIndex != -1)
            {
                if (opIndex == keys.Length - 1 || !int.TryParse(keys[opIndex + 1], out _))
                {
                    result.IsValid = false;
                    result.Message = "There must be at least one digit after the operation";
                    return result;
                }
            }

            result.IsValid = true;
            return result;
        }


        public bool IsOperator(string key)
        {
            return key == "+" || key == "-" || key == "*" || key == "/";
        }
    }
}
