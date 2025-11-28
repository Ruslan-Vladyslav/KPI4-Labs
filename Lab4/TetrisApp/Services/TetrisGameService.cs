using TetrisApp.Core;
using TetrisApp.Services.Interfaces;

namespace TetrisApp.Services
{
    public class TetrisGameService
    {
        private readonly IScreenReader _reader;
        private readonly IScreenWriter _writer;
        private readonly IErrorReporter _reporter;
        private readonly IGame _engine;

        public TetrisGameService(
            IScreenReader reader,
            IScreenWriter writer,
            IErrorReporter reporter,
            IGame engine)
        {
            _reader = reader;
            _writer = writer;
            _reporter = reporter;
            _engine = engine;
        }

        public void Run(string filePath, bool printAllSteps)
        {
            GameScreen initialScreen;

            try
            {
                initialScreen = _reader.ReadScreen(filePath);
            }
            catch (IO.InvalidDataException)
            {
                _reporter.ReportError();
                return;
            }

            IEnumerable<GameScreen> screens = _engine.DropPiece(initialScreen, printAllSteps);

            if (printAllSteps)
            {
                int step = 0;
                foreach (var screen in screens)
                {
                    _writer.WriteMessage($"STEP {step}");
                    _writer.WriteScreen(screen);
                    step++;
                }
            }
            else
            {
                _writer.WriteScreen(screens.Last());
            }
        }
    }
}
