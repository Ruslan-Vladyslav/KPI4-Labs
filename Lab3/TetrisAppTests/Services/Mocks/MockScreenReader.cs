using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services.Mocks
{
    public class MockScreenReader : IScreenReader
    {
        public GameScreen ResultToReturn { get; set; } = null!;
        public bool ShouldThrowException { get; set; } = false;
        public int CallCount { get; private set; } = 0;

        public GameScreen ReadScreen(string filePath)
        {
            CallCount++;
            if (ShouldThrowException) 
                throw new TetrisApp.IO.InvalidDataException("Mock Error.");

            return ResultToReturn;
        }
    }
}
