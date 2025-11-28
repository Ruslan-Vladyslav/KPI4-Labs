using TetrisApp.Core;
using TetrisApp.IO;

namespace TetrisAppTests.IO
{
    public class FileReaderTests : IDisposable
    {
        private const string TestFilePath = "test_file_reader.txt";
        private readonly FileReader _reader = new FileReader();

        public void Dispose()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        private void CreateTestFile(string content)
        {
            File.WriteAllText(TestFilePath, content);
        }

        [Fact]
        public void ReadScreen_ParseValidInput()
        {
            var content =
        @"3 3
.p.
#..
..."; 

            CreateTestFile(content);
            var screen = _reader.ReadScreen(TestFilePath);

            Assert.Equal(3, screen.Width);
            Assert.Equal(3, screen.Height);
            Assert.Contains(new Point(1, 0), screen.Piece);
            Assert.Contains(new Point(0, 1), screen.Landscape);
        }

        [Fact]
        public void ReadScreen_ExceptionMissingFile()
        {
            if (File.Exists(TestFilePath)) 
                File.Delete(TestFilePath);

            Assert.Throws<TetrisApp.IO.InvalidDataException>(() => _reader.ReadScreen(TestFilePath));
        }

        [Fact]
        public void ReadScreen_ExceptionIncorrectRow()
        {
            CreateTestFile("5 5\n.....\n.....\n.....\n.....");

            Assert.Throws<TetrisApp.IO.InvalidDataException>(() => _reader.ReadScreen(TestFilePath));
        }

        [Fact]
        public void ReadScreen_ExceptionMissingPiece()
        {
            CreateTestFile("3 3\n...\n#..\n...");

            Assert.Throws<TetrisApp.IO.InvalidDataException>(() => _reader.ReadScreen(TestFilePath));
        }
    }
}
