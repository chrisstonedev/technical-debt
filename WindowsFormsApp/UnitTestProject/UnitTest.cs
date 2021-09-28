using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            const int expected = 8;
            var model = new Mock<IModel>();
            model.Setup(x => x.GetFive()).Returns(expected);
            var newMethods = new NewMethods(model.Object);

            var result = newMethods.ReturnFive();

            Assert.AreEqual(expected, result);
        }
    }
}
