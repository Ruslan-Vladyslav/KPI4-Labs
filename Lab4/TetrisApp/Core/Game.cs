using TetrisApp.Services.Interfaces;

namespace TetrisApp.Core
{
    public class Game : IGame
    {
        public IEnumerable<GameScreen> DropPiece(GameScreen initialScreen, bool printAllSteps)
        {
            GameScreen currentScreen = initialScreen;
            List<GameScreen> states = new List<GameScreen>();

            if (printAllSteps)
            {
                states.Add(currentScreen);
            }

            while (true)
            {
                GameScreen? nextScreen = MoveDown(currentScreen);

                if (nextScreen == null)
                {
                    if (printAllSteps)
                    {
                        return states;
                    }
                    else
                    {
                        return new List<GameScreen> { currentScreen };
                    }
                }

                currentScreen = nextScreen;

                if (printAllSteps)
                {
                    states.Add(currentScreen);
                }
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
