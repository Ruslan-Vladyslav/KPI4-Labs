using TetrisApp.Core;

namespace TetrisAppTests.Core
{
    public class CoreTests
    {
        #region PointTests
        [Fact]
        public void Move_NewPoint()
        {
            var original = new Point(5, 10);
            var moved = original.Move(3, -2);

            Assert.Equal(8, moved.X);
            Assert.Equal(8, moved.Y);
            Assert.Equal(5, original.X);
        }

        [Fact]
        public void Equals_IdenticalPoints()
        {
            var p1 = new Point(2, 5);
            var p2 = new Point(2, 5);

            Assert.True(p1.Equals(p2));
            Assert.True(p1 == p2);
        }

        [Fact]
        public void GetHashCode_Equal()
        {
            var p1 = new Point(7, 3);
            var p2 = new Point(7, 3);

            Assert.Equal(p1.GetHashCode(), p2.GetHashCode());
        }
        #endregion


        #region GameScreen
        private GameScreen CreateScreen(List<Point> piece, List<Point> landscape)
        {
            return new GameScreen(10, 10, piece, landscape);
        }

        [Fact]
        public void IsCollision()
        {
            var landscape = new List<Point> { new Point(5, 8), new Point(6, 8) };
            var piece = new List<Point> { new Point(5, 7), new Point(6, 7) };
            var screen = CreateScreen(piece, landscape);

            Assert.False(screen.IsCollision(piece));
        }

        [Fact]
        public void IsCollision_PieceOverlap()
        {
            var landscape = new List<Point> { new Point(5, 8), new Point(6, 8) };
            var piece = new List<Point> { new Point(5, 8), new Point(7, 7) }; // Intersection
            var screen = CreateScreen(piece, landscape);

            Assert.True(screen.IsCollision(piece));
        }

        [Fact]
        public void IsOutOfBounds_PieceHitBottom()
        {
            var piece = new List<Point> { new Point(5, 9), new Point(6, 10) };
            var screen = CreateScreen(piece, new List<Point>());

            Assert.True(screen.IsOutOfBounds(piece));
        }

        [Fact]
        public void IsOutOfBounds_PieceHitSide()
        {
            var piece = new List<Point> { new Point(9, 5), new Point(10, 5) };
            var screen = CreateScreen(piece, new List<Point>());

            Assert.True(screen.IsOutOfBounds(piece));
        }

        [Fact]
        public void IsOutOfBounds_PieceWithinBounds()
        {
            var piece = new List<Point> { new Point(0, 0), new Point(9, 9) };
            var screen = CreateScreen(piece, new List<Point>());

            Assert.False(screen.IsOutOfBounds(piece));
        }
        #endregion


        #region GameEngine
        private readonly Game _engine = new Game();

        private GameScreen CreateScreen(int h, int w, List<Point> piece, List<Point> landscape)
        {
            return new GameScreen(w, h, piece, landscape);
        }

        [Fact]
        public void DropPiece_NormalMode_ReturnsFinalScreenOnly()
        {
            var piece = new List<Point> { new Point(2, 0) };
            var screen = CreateScreen(5, 5, piece, new List<Point>());

            var finalScreens = _engine.DropPiece(screen, false);
            var finalScreen = finalScreens.First(); 

            Assert.Single(finalScreens);
            Assert.Equal(4, finalScreen.Piece.First().Y);
        }

        [Fact]
        public void DropPiece_VerboseMode_ReturnsAllSteps()
        {
            var piece = new List<Point> { new Point(2, 0) };
            var screen = CreateScreen(5, 5, piece, new List<Point>());

            var allSteps = _engine.DropPiece(screen, true).ToList();

            Assert.Equal(5, allSteps.Count);
            Assert.Equal(0, allSteps[0].Piece.First().Y);
            Assert.Equal(1, allSteps[1].Piece.First().Y); 
            Assert.Equal(4, allSteps.Last().Piece.First().Y);
        }

        [Fact]
        public void DropPiece_StopAbove_VerboseMode()
        {
            var piece = new List<Point> { new Point(2, 0) };
            var landscape = new List<Point> { new Point(2, 3) };
            var screen = CreateScreen(5, 5, piece, landscape);

            var allSteps = _engine.DropPiece(screen, true).ToList();
            var finalScreen = allSteps.Last();

            Assert.Equal(3, allSteps.Count);
            Assert.Equal(2, finalScreen.Piece.First().Y);
        }
        #endregion
    }
}
