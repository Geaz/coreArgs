using Microsoft.VisualStudio.TestTools.UnitTesting;
using coreArgs;
using coreArgs.Tests.Options;

namespace coreArgs.Tests
{
    [TestClass]
    public class NumberParsingTests
    {
        [TestMethod]
        public void ShouldParseDecimalArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "-d", "2.2" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual(2.2m, result.Arguments.DecimalOption);
        }

        [TestMethod]
        public void ShouldParseIntArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "-i", "10" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual(10, result.Arguments.IntOption);
        }

        [TestMethod]
        public void ShouldParseDoubleArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--double", "10.2" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual(10.2d, result.Arguments.DoubleOption);
        }

        [TestMethod]
        public void ShouldParseIntListArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--intlist", "1, 2, 3, 4" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual("1, 2, 3, 4", string.Join(", ", result.Arguments.ListIntOptions));
        }

        [TestMethod]
        public void ShouldParseDecimalListArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--decimallist", "1.1, 2.2, 3.3, 4.4" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual(1.1m, result.Arguments.ListDecimalOptions[0]);
            Assert.AreEqual(2.2m, result.Arguments.ListDecimalOptions[1]);
            Assert.AreEqual(3.3m, result.Arguments.ListDecimalOptions[2]);
            Assert.AreEqual(4.4m, result.Arguments.ListDecimalOptions[3]);
        }

        [TestMethod]
        public void ShouldParseDoubleListArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--doublelist", "1.1, 2.2, 3.3, 4.4" };

            //Act
            var result = ArgsParser.Parse<NumberOptions>(args);

            //Assert
            Assert.AreEqual(1.1d, result.Arguments.ListDoubleOptions[0]);
            Assert.AreEqual(2.2d, result.Arguments.ListDoubleOptions[1]);
            Assert.AreEqual(3.3d, result.Arguments.ListDoubleOptions[2]);
            Assert.AreEqual(4.4d, result.Arguments.ListDoubleOptions[3]);
        }
    }
}
