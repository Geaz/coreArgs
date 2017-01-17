using Microsoft.VisualStudio.TestTools.UnitTesting;
using coreArgs;

namespace Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ShouldParseLongArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "--longstring", "test" };

            //Act
            var options = ArgsParser.Parse<TestOptions>(args);

            //Assert
            Assert.AreEqual("test", options.LongStringOption);
        }

        [TestMethod]
        public void ShouldParseBinArgumentSuccessfully()
        {
            //Arrange
            var args = new [] { "binOption" };

            //Act
            var options = ArgsParser.Parse<TestOptions>(args);

            //Assert
            Assert.IsTrue(options.BinOption.Contains("binOption"));
        }
    }
}
