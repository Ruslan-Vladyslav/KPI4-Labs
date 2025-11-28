using TetrisApp.Core;

namespace TetrisApp.Services.Interfaces
{
    public interface IGame
    {
        IEnumerable<GameScreen> DropPiece(GameScreen screen, bool printAllSteps);
    }
}
