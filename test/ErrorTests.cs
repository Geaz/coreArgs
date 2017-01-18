using Microsoft.VisualStudio.TestTools.UnitTesting;

using coreArgs.Tests.Options;

namespace coreArgs.Tests
{
    [TestClass]
    public class ErrorTests
    {
        [TestMethod]
        public void ShouldReturnErrorOnRequiredMissing()
        {
            //Arrange
            var args = new [] { "" };

            //Act
            var result = ArgsParser.Parse<RequiredOptions>(args);
                      
            //Assert
            Assert.IsTrue(result.Errors.Count > 0);
        }
    }
}
