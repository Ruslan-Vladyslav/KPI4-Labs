using TetrisApp.Core;

namespace TetrisApp.Services.Interfaces
{
    public interface IScreenWriter
    {
        void WriteScreen(GameScreen finalScreen);
        void WriteMessage(string message);
    }
}
