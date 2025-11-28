using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services.Mocks
{
    public class MockGame : IGame
    {
        public IEnumerable<GameScreen> FinalResult { get; set; } = null!;

        public IEnumerable<GameScreen> DropPiece(GameScreen screen, bool printAllSteps)
        {
            return FinalResult;
        }
    }
}
