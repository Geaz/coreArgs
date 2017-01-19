using Microsoft.VisualStudio.TestTools.UnitTesting;

using coreArgs.Tests.Options;

namespace coreArgs.Tests
{
    [TestClass]
    public class HelpTextTests
    {
        //[TestMethod]
        public void ShouldReturnCorrectHelpText()
        {
            //Act
            var helpText = ArgsParser.GetHelpText<RequiredOptions>();
                      
            //Assert
            Assert.AreEqual("     --longstring\t\tThe longoption (required)\r\n", helpText);
        }
    }
}
