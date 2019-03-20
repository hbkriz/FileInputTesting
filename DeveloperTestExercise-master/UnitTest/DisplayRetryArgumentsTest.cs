using System;
using FileData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class DisplayRetryArgumentsTest
    {
        private FileInformation _fileInfoInstance;
        private PrivateObject _fileInfoClass;
        private string _displayRetriedArgumentsFunction;
        private string[] _incorrectNewArgsForRetry;
        private object _existingValidArgs;
        private string[] _correctNewArgsForRetry;

        [TestInitialize]
        public void Initialize()
        {
            _fileInfoInstance = new FileInformation();
            _fileInfoClass = new PrivateObject(_fileInfoInstance);
            _displayRetriedArgumentsFunction = "DisplayRetryArguments";
            SetupMock();
        }

        private void SetupMock()
        {
            _existingValidArgs = new[] { "-v", "C:/Test.txt" };
            
            _incorrectNewArgsForRetry = new[] {
                " ",
                "-v -s C:/Test.txt",
                "-s C:/Test.txt aaa",
                string.Empty,
                null,
                "-test C:/Test.txt"
            };

            _correctNewArgsForRetry = new[] {
                "-s C:/Test.txt",
                "--s C:/Test.txt",
                "/s C:/Test.txt",
                "-v C:/Test.txt",
                "--v C:/Test.txt",
                "/v C:/Test.txt",
                "--version C:/Test.txt"
            };

        }

       
        [TestMethod]
        public void IncorrectArgumentsShouldDisplayRetryTest()
        {
            foreach (var incorrectArgs in _incorrectNewArgsForRetry)
            {
                var args = new[] { _existingValidArgs, incorrectArgs };
                var result = _fileInfoClass.Invoke(_displayRetriedArgumentsFunction, args);
                Assert.AreEqual(result, true);
            }
        }

        [TestMethod]
        public void CorrectArgumentsShouldNotDisplayRetryTest()
        {
            foreach (var correctArgs in _correctNewArgsForRetry)
            {
                var args = new[] { _existingValidArgs, correctArgs };
                var result = _fileInfoClass.Invoke(_displayRetriedArgumentsFunction, args);
                Assert.AreEqual(result, false);
            }
        }
    }
}
