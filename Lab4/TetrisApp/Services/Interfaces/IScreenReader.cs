using TetrisApp.Core;

namespace TetrisApp.Services.Interfaces
{
    public interface IScreenReader
    {
        GameScreen ReadScreen(string filePath);
    }
}
