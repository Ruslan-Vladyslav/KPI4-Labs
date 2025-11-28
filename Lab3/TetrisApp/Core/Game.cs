using TetrisApp.Services.Interfaces;

namespace TetrisApp.Core
{
    public class Game : IGame
    {
        public GameScreen DropPiece(GameScreen initialScreen)
        {
            GameScreen currentScreen = initialScreen;
            GameScreen nextScreen;

            while (true)
            {
                nextScreen = MoveDown(currentScreen)!;

                if (nextScreen == null)
                {
                    return currentScreen;
                }

                currentScreen = nextScreen;
            }
        }

        private GameScreen? MoveDown(GameScreen screen)
        {
            List<Point> newPiecePoints = screen.Piece
                .Select(p => p.Move(0, 1))
                .ToList();

            if (screen.IsOutOfBounds(newPiecePoints) || screen.IsCollision(newPiecePoints))
            {
                return null;
            }

            return new GameScreen(
                screen.Width,
                screen.Height,
                newPiecePoints,
                screen.Landscape
            );
        }
    }
}
