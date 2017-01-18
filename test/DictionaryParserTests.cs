using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using coreArgs.Parser;

namespace coreArgs.Tests
{
    [TestClass]
    public class DictionaryParserTests
    {
        [TestMethod]
        public void ShouldParseLongOptionSuccessFully()
        {
            //Arrange
            var args = new [] { "--longoption", "test" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual("test", dictionary["longoption"]);
        }

        [TestMethod]
        public void ShouldParseShortOptionSuccessFully()
        {
            //Arrange
            var args = new [] { "-l", "test" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual("test", dictionary["l"]);
        }

        [TestMethod]
        public void ShouldParseBoolLongOptionSuccessFully()
        {
            //Arrange
            var args = new [] { "--bool" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual("true", dictionary["bool"]);
        }

        [TestMethod]
        public void ShouldParseBoolShortOptionSuccessFully()
        {
            //Arrange
            var args = new [] { "-b" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual("true", dictionary["b"]);
        }

        [TestMethod]
        public void ShouldParseBinOptionSuccessFully()
        {
            //Arrange
            var args = new [] { "binOption" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual(string.Empty, dictionary["binOption"]);
        }

        [TestMethod]
        public void ShoudParseManySuccessfully()
        {
            //Arrange
            var args = new [] { "-b", "--longoption", "test", "binOption", "-d", "12" };
            var dicParser = new DictionaryParser();

            //Act
            var dictionary = dicParser.ParseArgumentsIntoDic(args);

            //Assert
            Assert.AreEqual("true", dictionary["b"]);
            Assert.AreEqual("test", dictionary["longoption"]);
            Assert.AreEqual(string.Empty, dictionary["binOption"]);
            Assert.AreEqual("12", dictionary["d"]);
        }
    }
}
