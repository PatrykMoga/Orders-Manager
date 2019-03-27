using Autofac.Extras.Moq;
using NUnit.Framework;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using System.Collections.Generic;

namespace OrdersManager.Tests.DeserializersTests
{
    [TestFixture]
    public class XmlDeserializerTests
    {
        private readonly IEnumerable<string> files = new List<string>
        {
            @"..\..\..\TestingFiles\file.xml",
            @"..\..\..\TestingFiles\file2.xml"
        };

        [Test]
        public void DeserializeFiles_Scenario_ExpectedBehavior()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var sut = mock.Create<CsvDeserializer>();
                var acutal = sut.DeserializeFiles(files);

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "5", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 21},
                    new Request{ ClientId = "6", RequestId = 2, Name = "Chleb", Price = 15M, Quantity = 2},
                    new Request{ ClientId = "5", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 21},
                    new Request{ ClientId = "6", RequestId = 2, Name = "Chleb", Price = 15M, Quantity = 2}
                };

                for (int i = 0; i < acutal.Count; i++)
                {
                    Assert.That(acutal[i].ClientId == expected[i].ClientId);
                    Assert.That(acutal[i].Name == expected[i].Name);
                    Assert.That(acutal[i].Price == expected[i].Price);
                    Assert.That(acutal[i].Quantity == expected[i].Quantity);
                    Assert.That(acutal[i].RequestId == expected[i].RequestId);
                }
            }
        }

        [Test]
        public void DeserializeFile_Scenario_ExpectedBehavior()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var sut = mock.Create<CsvDeserializer>();
                var acutal = sut.DeserializeFile(@"..\..\..\TestingFiles\file.xml");

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "5", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 21},
                    new Request{ ClientId = "6", RequestId = 2, Name = "Chleb", Price = 15M, Quantity = 2}
                };

                for (int i = 0; i < acutal.Count; i++)
                {
                    Assert.That(acutal[i].ClientId == expected[i].ClientId);
                    Assert.That(acutal[i].Name == expected[i].Name);
                    Assert.That(acutal[i].Price == expected[i].Price);
                    Assert.That(acutal[i].Quantity == expected[i].Quantity);
                    Assert.That(acutal[i].RequestId == expected[i].RequestId);
                }
            }
        }
    }
}
