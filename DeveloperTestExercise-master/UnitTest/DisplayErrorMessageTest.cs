using System;
using FileData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class DisplayErrorMessageTest
    {
        private static FileInformation _fileInfoInstance;
        private PrivateObject _fileInfoClass;
        private string _displayErrorMessageFunction;
        private object[] _correctSizeArgs;
        private object[] _correctVersionArgs;
        private object[] _incorrectArgs;

        [TestInitialize]
        public void Initialize()
        {
            _fileInfoInstance = new FileInformation();
            _fileInfoClass = new PrivateObject(_fileInfoInstance);
            _displayErrorMessageFunction = "DisplayErrorMessage";
            SetupMock();
        }

        private void SetupMock()
        {
            _correctSizeArgs = new object[] {
                new[] { "-s", "C:/Test.txt" },
                new[] { "--s", "C:/Test.txt" },
                new[] { "/s", "C:/Test.txt" },
                new[] { "--size", "C:/Test.txt" }
            };

            _correctVersionArgs = new object[] {
                new[] { "-v", "C:/Test.txt" },
                new[] { "--v", "C:/Test.txt" },
                new[] { "/v", "C:/Test.txt" },
                new[] { "--version", "C:/Test.txt" }
            };

            _incorrectArgs = new object[] {
                new[] { "--versn", "C:/Test.txt" },
                new[] { "s", "C:/Test.txt" },
                new[] { "/v", "AAA" },
                new[] { "/v", "AAA", "BB" },
                new[] { "/v", "-s", "C:/Test.txt" },
                new[] { " ", " " },
                new[] { "", "" },
                new[] { "" }
            };
        }

        [TestMethod]
        public void IncorrectArgumentsShouldShowTrue()
        {
            foreach (var args in _incorrectArgs)
            {
                var result = _fileInfoClass.Invoke(_displayErrorMessageFunction, args);
                Assert.AreEqual(result, true);
            }
        }
        [TestMethod]
        public void CorrectSizeArgumentsShouldShowFalse()
        {
            foreach (var args in _correctSizeArgs)
            {
                var result = _fileInfoClass.Invoke(_displayErrorMessageFunction, args);
                Assert.AreEqual(result, false);
            }
        }
        [TestMethod]
        public void CorrectVersionArgumentsShouldShowFalse()
        {
            foreach (var args in _correctVersionArgs)
            {
                var result = _fileInfoClass.Invoke(_displayErrorMessageFunction, args);
                Assert.AreEqual(result, false);
            }
        }
    }
}
