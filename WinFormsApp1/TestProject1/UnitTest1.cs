using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WinFormsApp1;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var model = new Mock<IModel>();
            model.Setup(x => x.Variance).Returns(7);
            var view = new Mock<IView>();
            var presenter = new Presenter(model.Object, view.Object);

            var result = presenter.Test();

            Assert.AreEqual(result, 7);

        }
    }
}
