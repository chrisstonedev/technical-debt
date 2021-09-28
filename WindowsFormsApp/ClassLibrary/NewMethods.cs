using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ClassLibrary
{
    [Guid("CC9A9CBC-054A-4C9C-B559-CE39A5EA2742")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class NewMethods
    {
        private readonly IModel model;

        public NewMethods()
        {
            model = new Model();
        }

        public NewMethods(IModel model)
        {
            this.model = model;
        }

        public string CreateXmlWithCustomFirstName(string customFirstName)
        {
            var xDocument = new XDocument();
            var xElement = new XElement("Myinfo");
            xDocument.Add(xElement);
            xElement.Add(new XElement("FirstName", "My First Name"));
            xElement.Add(new XElement("LastName", "My Last Name"));
            xElement.Add(new XElement("StreetAdd", "My Address"));

            if (!string.IsNullOrEmpty(customFirstName))
            {
                xDocument.Root.Element("FirstName").Value = customFirstName;
            }

            return xDocument.ToString();
        }
    }
}
