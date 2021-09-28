using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Xml.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateXmlWithActualStringTest()
        {
            var model = new Mock<IModel>();
            var newMethods = new NewMethods(model.Object);

            var result = newMethods.CreateXmlWithCustomFirstName("Chris");

            var xml = XDocument.Parse(result);
             
            Assert.AreEqual("Myinfo", xml.Root.Name);
            Assert.AreEqual("Chris", xml.Root.Element("FirstName").Value);
            Assert.AreEqual("My Last Name", xml.Root.Element("LastName").Value);
            Assert.AreEqual("My Address", xml.Root.Element("StreetAdd").Value);
        }

        [TestMethod]
        public void CreateXmlWithNullStringTest()
        {
            var model = new Mock<IModel>();
            var newMethods = new NewMethods(model.Object);

            var result = newMethods.CreateXmlWithCustomFirstName(null);

            var xml = XDocument.Parse(result);
             
            Assert.AreEqual("Myinfo", xml.Root.Name);
            Assert.AreEqual("My First Name", xml.Root.Element("FirstName").Value);
            Assert.AreEqual("My Last Name", xml.Root.Element("LastName").Value);
            Assert.AreEqual("My Address", xml.Root.Element("StreetAdd").Value);
        }
    }
}
