using TetrisApp.Core;

namespace TetrisApp.Services.Interfaces
{
    public interface IGame
    {
        GameScreen DropPiece(GameScreen screen);
    }
}
