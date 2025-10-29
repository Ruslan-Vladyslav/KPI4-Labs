
namespace CalculatorTests
{
    using Calculator;
    using System;
    using System.IO;
    using Xunit;

    namespace CalculatorTests
    {
        public class IOManagerTest : IDisposable
        {
            private readonly string _tempFile;

            public IOManagerTest()
            {
                _tempFile = Path.GetTempFileName();
            }

            public void Dispose()
            {
                if (File.Exists(_tempFile))
                {
                    File.Delete(_tempFile);
                }

                if (File.Exists("output.txt"))
                {
                    File.Delete("output.txt");
                }
            }

            [Fact]
            public void GetInput_FromFile_ReturnsContent()
            {
                File.WriteAllText(_tempFile, "1 2 + 3 =");

                string[] args = new[] { _tempFile };
                string input = IOManager.GetInput(args);

                Assert.Equal("1 2 + 3 =", input);
            }

            [Fact]
            public void HandleOutput_WritesToFile()
            {
                string[] args = new string[0];
                int result = 123;

                IOManager.HandleOutput(args, result);

                Assert.True(File.Exists("output.txt"));
                string content = File.ReadAllText("output.txt");
                Assert.Equal("123", content);
            }
        }
    }

}
