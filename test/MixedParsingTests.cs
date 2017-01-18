using Microsoft.VisualStudio.TestTools.UnitTesting;
using coreArgs;
using coreArgs.Tests.Options;

namespace coreArgs.Tests
{
    [TestClass]
    public class MixedParsingTests
    {
        [TestMethod]
        public void ShouldParseOverrideOnMultiArgumentsSuccessfully()
        {
            //Arrange
            var args = new [] { "-s", "test", "--shortoption", "test2" };

            //Act
            var result = ArgsParser.Parse<MixedOptions>(args);

            //Assert
            Assert.AreEqual("test2", result.Arguments.ShortStringOption);
        }

        [TestMethod]
        public void ShouldParseRemainingArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--notdefined", "test" };

            //Act
            var result = ArgsParser.Parse<MixedOptions>(args);

            //Assert
            Assert.AreEqual("test", result.Arguments.RemainingOptions.notdefined);
        }

        [TestMethod]
        public void ShouldReturnNullForNotDefinedRemainingSuccessfully()
        {
            //Arrange
            var args = new [] { "--another", "test" };

            //Act
            var result = ArgsParser.Parse<MixedOptions>(args);

            //Assert
            Assert.AreEqual("test", result.Arguments.RemainingOptions.another);            
            Assert.IsNotNull(result.Arguments.RemainingOptions.another);
            
            Assert.AreEqual("", $"{result.Arguments.RemainingOptions.anotherNotDefined}");
            Assert.IsNull(result.Arguments.RemainingOptions.anotherNotDefined);
        }

        [TestMethod]
        public void ShouldParseBinArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "binOption" };

            //Act
            var result = ArgsParser.Parse<MixedOptions>(args);

            //Assert
            Assert.IsTrue(result.Arguments.BinOption.Contains("binOption"));
        }
    }
}
