using NUnit.Framework;
using OrderCore.Server.Functions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OrderCore.ServerTests
{
    public class Tests
    {
        [Test]
        public void ProcessCompleteOrderRequestTest()
        {
            // Arrange.
            var server = new NewMethods();
            var data = "<xml><response id=\"C\">0142</response><response id=\"P\">baba</response></xml>";

            // Act.
            var result = server.ProcessCompleteOrderRequest(data);

            // Assert.
            Assert.That(result.Length, Is.EqualTo(52));
            Assert.That(result[..10], Is.EqualTo("0142      "));
            Assert.That(result[29..], Is.EqualTo("        baba           "));
        }

        [Test]
        public void ParseXmlFailureTest()
        {
            // Arrange.
            var server = new NewMethods();
            var data = "<xml<response id=\"C\">0142</response><response id=\"P\">baba</response></xml>";

            // Act.
            var result = server.ProcessCompleteOrderRequest(data);

            // Assert.
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ConvertXmlElementToResponseTest()
        {
            // Arrange.
            var server = new NewMethods();
            var data = "<xml><response id=\"C\">0142</response><response id=\"P\">baba</response></xml>";
            var xml = XElement.Parse(data);

            // Act. XElement element
            var responses = server.ConvertXmlToResponses(xml);

            // Assert.
            Assert.That(responses, Is.Not.Null);
            Assert.That(responses, Has.Count.EqualTo(2));
            Assert.That(responses[0].FieldId, Is.EqualTo("C"));
            Assert.That(responses[0].UserResponse, Is.EqualTo("0142"));
            Assert.That(responses[1].FieldId, Is.EqualTo("P"));
            Assert.That(responses[1].UserResponse, Is.EqualTo("baba"));
        }

        [Test]
        public void UseResponsesToBuildDataBufferTest()
        {
            // Arrange.
            var server = new NewMethods();
            var responses = new List<Response>
            {
                new Response
                {
                    FieldId = "C",
                    UserResponse = "0142"
                },
                new Response
                {
                    FieldId = "P",
                    UserResponse = "baba"
                },
            };

            // Act. XElement element
            var result = server.UseResponsesToBuildOrder(responses);

            // Assert.
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Customer, Is.EqualTo("0142      "));
            Assert.That(result.DateOrdered, Does.Contain(DateTime.Now.ToString("yyyy-MM-dd")));
            Assert.That(result.Price, Is.EqualTo("        "));
            Assert.That(result.Product, Is.EqualTo("baba      "));
            Assert.That(result.Quantity, Is.EqualTo("     "));
        }
    }
}
