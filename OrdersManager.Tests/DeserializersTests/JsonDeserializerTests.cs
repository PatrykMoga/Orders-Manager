using Autofac.Extras.Moq;
using NUnit.Framework;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using System.Collections.Generic;

namespace OrdersManager.Tests.DeserializersTests
{
    [TestFixture]
    public class JsonDeserializerTests
    {
        private readonly IEnumerable<string> files = new List<string>
        {
            @"..\..\..\TestingFiles\file.json",
            @"..\..\..\TestingFiles\file2.json"
        };

        [Test]
        public void DeserializeFiles_Scenario_ExpectedBehavior()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var sut = mock.Create<JsonDeserializer>();
                var acutal = sut.DeserializeFiles(files);

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "3", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 6},
                    new Request{ ClientId = "4", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 7},
                    new Request{ ClientId = "3", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 6},
                    new Request{ ClientId = "4", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 7}
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
                var sut = mock.Create<JsonDeserializer>();
                var acutal = sut.DeserializeFile(@"..\..\..\TestingFiles\file.json");

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "3", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 6},
                    new Request{ ClientId = "4", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 7}
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
