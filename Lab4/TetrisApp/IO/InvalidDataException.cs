namespace TetrisApp.IO
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException() 
            : base("Input data format is incorrect.") 
        { 
        }

        public InvalidDataException(string message)
            : base(message)
        {
        }

        public InvalidDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
