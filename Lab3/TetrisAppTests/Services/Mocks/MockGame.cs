using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services.Mocks
{
    public class MockGame : IGame
    {
        public GameScreen FinalResult { get; set; } = null!;
        public GameScreen DropPiece(GameScreen screen) => FinalResult;
    }
}
