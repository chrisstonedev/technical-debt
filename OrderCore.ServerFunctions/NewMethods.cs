using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;

namespace OrderCore.ServerFunctions
{
    [Guid("CC9A9CBC-054A-4C9C-B559-CE39A5EA2742")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class NewMethods
    {
        public string ProcessCompleteOrderRequest(string data)
        {
            XElement xml;
            try
            {
                xml = XElement.Parse(data);
            }
            catch (XmlException)
            {
                return string.Empty;
            }
            var responses = ConvertXmlToResponses(xml);
            var order = UseResponsesToBuildOrder(responses);
            return order.ToDatabaseFormat();
        }

        internal List<Response> ConvertXmlToResponses(XElement xmlData)
        {
            return xmlData.Elements().Select(x => new Response
            {
                FieldId = x.Attribute("id").Value,
                UserResponse = x.Value,
            }).ToList();
        }

        internal Order UseResponsesToBuildOrder(List<Response> responses)
        {
            var order = new Order();
            if (responses.Any(x => x.FieldId == "C"))
                order.Customer = responses.First(x => x.FieldId == "C").UserResponse;
            if (responses.Any(x => x.FieldId == "P"))
                order.Product = responses.First(x => x.FieldId == "P").UserResponse;
            if (responses.Any(x => x.FieldId == "Q"))
                order.Quantity = responses.First(x => x.FieldId == "Q").UserResponse;
            if (responses.Any(x => x.FieldId == "R"))
                order.Price = responses.First(x => x.FieldId == "R").UserResponse;
            return order;
        }
    }
}
