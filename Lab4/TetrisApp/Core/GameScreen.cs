
namespace TetrisApp.Core
{
    public class GameScreen
    {
        public readonly int Width;
        public readonly int Height;
        public readonly List<Point> Piece;
        public readonly List<Point> Landscape;

        public GameScreen(int width, int height, List<Point> piece, List<Point> landscape)
        {
            Width = width;
            Height = height;
            Piece = piece;
            Landscape = landscape;
        }

        public bool IsCollision(List<Point> piecePoints)
        {
            return piecePoints.Any(p => Landscape.Contains(p));
        }

        public bool IsOutOfBounds(List<Point> piecePoints)
        {
            return piecePoints.Any(p => p.Y >= Height || p.X < 0 || p.X >= Width);
        }
    }
}
