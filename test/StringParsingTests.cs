using Microsoft.VisualStudio.TestTools.UnitTesting;

using coreArgs.Tests.Options;

namespace coreArgs.Tests
{
    [TestClass]
    public class StringParsingTests
    {
        [TestMethod]
        public void ShouldParseLongArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--longstring", "test" };

            //Act
            var result = ArgsParser.Parse<StringOptions>(args);

            //Assert
            Assert.AreEqual("test", result.Arguments.LongStringOption);
        }

        [TestMethod]
        public void ShouldParseShortArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "-s", "test" };

            //Act
            var result = ArgsParser.Parse<StringOptions>(args);

            //Assert
            Assert.AreEqual("test", result.Arguments.ShortStringOption);
        }

        [TestMethod]
        public void ShouldParseListArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "-l", "test, test2, test3" };

            //Act
            var result = ArgsParser.Parse<StringOptions>(args);

            //Assert
            Assert.AreEqual("test, test2, test3", string.Join(", ", result.Arguments.ListStringOptions));
        }
    }
}
