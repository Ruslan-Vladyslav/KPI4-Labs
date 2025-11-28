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

        public void Run(string filePath)
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

            _writer.WriteScreen(initialScreen);
            GameScreen finalScreen = _engine.DropPiece(initialScreen);
            _writer.WriteScreen(finalScreen);
        }
    }
}
