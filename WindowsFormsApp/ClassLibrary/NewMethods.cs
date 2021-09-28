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
            var xDocument = new XDocument(new XElement("Myinfo",
                new XElement("FirstName", !string.IsNullOrEmpty(customFirstName) ? customFirstName : "My First Name"),
                new XElement("LastName", "My Last Name"),
                new XElement("StreetAdd", "My Address")
            ));

            return xDocument.ToString();
        }
    }
}
