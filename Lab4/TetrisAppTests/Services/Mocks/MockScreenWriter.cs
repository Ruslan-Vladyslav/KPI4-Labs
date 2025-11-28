using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services.Mocks
{
    public class MockScreenWriter : IScreenWriter
    {
        public int CallCount { get; private set; } = 0;

        public void WriteMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteScreen(GameScreen finalScreen) => CallCount++;
    }
}
