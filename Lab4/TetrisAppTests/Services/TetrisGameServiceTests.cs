using NSubstitute; 
using TetrisApp.Core;
using TetrisApp.Services;
using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services
{
    public class TetrisGameServiceTests
    {
        private readonly GameScreen DefaultScreen = new GameScreen(10, 10, new List<Point>(), new List<Point>());
        private readonly GameScreen Step0 = new GameScreen(10, 10, new List<Point> { new Point(5, 0) }, new List<Point>());
        private readonly GameScreen Step1 = new GameScreen(10, 10, new List<Point> { new Point(5, 1) }, new List<Point>());
        private readonly GameScreen Step2 = new GameScreen(10, 10, new List<Point> { new Point(5, 2) }, new List<Point>());
        private readonly IEnumerable<GameScreen> DefaultFinalResult;
        private IEnumerable<GameScreen> Result => new List<GameScreen> { Step0, Step1, Step2 };

        public TetrisGameServiceTests()
        {
            DefaultFinalResult = new List<GameScreen> { DefaultScreen };
        }


        [Fact]
        public void Run_SuccessScenario_CallsReaderEngineAndWriterOnce()
        {
            var mockReader = Substitute.For<IScreenReader>();
            var mockWriter = Substitute.For<IScreenWriter>();
            var mockReporter = Substitute.For<IErrorReporter>();
            var mockEngine = Substitute.For<IGame>();

            mockReader.ReadScreen("valid.txt").Returns(DefaultScreen);
            mockEngine.DropPiece(DefaultScreen, false).Returns(DefaultFinalResult);

            var service = new TetrisGameService(mockReader, mockWriter, mockReporter, mockEngine);

            service.Run("valid.txt", false);


            mockReader.Received(1).ReadScreen("valid.txt");
            mockEngine.Received(1).DropPiece(DefaultScreen, false);
            mockWriter.Received(1).WriteScreen(DefaultFinalResult.Last());
            mockReporter.DidNotReceive().ReportError();
        }

        [Fact]
        public void Run_InvalidDataScenario_CallsReaderAndReporterButNotEngineOrWriter()
        {
            var mockReader = Substitute.For<IScreenReader>();
            var mockWriter = Substitute.For<IScreenWriter>();
            var mockReporter = Substitute.For<IErrorReporter>();
            var mockEngine = Substitute.For<IGame>();

            mockReader.ReadScreen(Arg.Any<string>()).Returns(x => {
                throw new TetrisApp.IO.InvalidDataException("Mock Error.");
            });

            var service = new TetrisGameService(mockReader, mockWriter, mockReporter, mockEngine);

            service.Run("invalid.txt", false);


            mockReporter.Received(1).ReportError();
            mockEngine.DidNotReceive().DropPiece(Arg.Any<GameScreen>(), Arg.Any<bool>());
            mockWriter.DidNotReceive().WriteScreen(Arg.Any<GameScreen>());
            mockWriter.DidNotReceive().WriteMessage(Arg.Any<string>());
        }

        [Fact]
        public void Show_StepsCorrectly()
        {
            var mockReader = Substitute.For<IScreenReader>();
            var mockWriter = Substitute.For<IScreenWriter>();
            var mockReporter = Substitute.For<IErrorReporter>();
            var mockEngine = Substitute.For<IGame>();

            mockReader.ReadScreen(Arg.Any<string>()).Returns(Step0);
            mockEngine.DropPiece(Step0, true).Returns(Result);

            var service = new TetrisGameService(mockReader, mockWriter, mockReporter, mockEngine);
            service.Run("verbose.txt", true);


            mockWriter.Received(3).WriteMessage(Arg.Any<string>());
            mockWriter.Received(3).WriteScreen(Arg.Any<GameScreen>());

            Received.InOrder(() =>
            {
                mockWriter.WriteMessage("STEP 0");
                mockWriter.WriteScreen(Step0);

                mockWriter.WriteMessage("STEP 1");
                mockWriter.WriteScreen(Step1);

                mockWriter.WriteMessage("STEP 2");
                mockWriter.WriteScreen(Step2);
            });

            mockEngine.Received(1).DropPiece(Step0, true);
        }
    }
}