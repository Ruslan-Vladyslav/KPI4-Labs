using TetrisApp.Services.Interfaces;

namespace TetrisAppTests.Services.Mocks
{
    public class MockErrorReporter : IErrorReporter
    {
        public int CallCount { get; private set; } = 0;
        public void ReportError() => CallCount++;
    }
}
