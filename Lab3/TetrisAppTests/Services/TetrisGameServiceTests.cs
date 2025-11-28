using TetrisApp.Core;
using TetrisApp.Services;
using TetrisAppTests.Services.Mocks;

namespace TetrisAppTests.Services
{
    public class TetrisGameServiceTests
    {
        [Fact]
        public void SuccessScenario()
        {
            var initialScreen = new GameScreen(10, 10, new List<Point> { new Point(5, 5) }, new List<Point>());
            var finalScreen = new GameScreen(10, 10, new List<Point>(), new List<Point> { new Point(5, 9) });

            var mockReader = new MockScreenReader { ResultToReturn = initialScreen };
            var mockWriter = new MockScreenWriter();
            var mockReporter = new MockErrorReporter();
            var mockEngine = new MockGame { FinalResult = finalScreen };

            var service = new TetrisGameService(mockReader, mockWriter, mockReporter, mockEngine);

            service.Run("valid_file.txt");

            Assert.Equal(1, mockReader.CallCount);
            Assert.Equal(2, mockWriter.CallCount);
            Assert.Equal(0, mockReporter.CallCount);
        }

        [Fact]
        public void InvalidDataScenario()
        {
            var mockReader = new MockScreenReader { ShouldThrowException = true };
            var mockWriter = new MockScreenWriter();
            var mockReporter = new MockErrorReporter();
            var mockEngine = new MockGame();
            var service = new TetrisGameService(mockReader, mockWriter, mockReporter, mockEngine);

            service.Run("invalid_file.txt");

            Assert.Equal(1, mockReader.CallCount);
            Assert.Equal(1, mockReporter.CallCount);
            Assert.Equal(0, mockWriter.CallCount);
        }
    }
}
