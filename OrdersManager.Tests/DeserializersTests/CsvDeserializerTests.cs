using Autofac.Extras.Moq;
using NUnit.Framework;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using System.Collections.Generic;

namespace OrdersManager.Tests.DeserializersTests
{
    [TestFixture]
    public class CsvDeserializerTests
    {
        private readonly IEnumerable<string> files = new List<string>
        {
            @"..\..\..\TestingFiles\file.csv",
            @"..\..\..\TestingFiles\file2.csv"
        };

        [Test]
        public void DeserializeFiles_Scenario_ExpectedBehavior()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //mock.Mock<IFilesReader>()
                //    .Setup(f => f.Files)
                //    .Returns(files);

                var sut = mock.Create<CsvDeserializer>();
                var acutal = sut.DeserializeFiles(files);

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "1", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 1},
                    new Request{ ClientId = "2", RequestId = 1, Name = "Chleb", Price = 15M, Quantity = 2},
                    new Request{ ClientId = "1", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 1},
                    new Request{ ClientId = "2", RequestId = 1, Name = "Chleb", Price = 15M, Quantity = 2}
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
                var acutal = sut.DeserializeFile(@"..\..\..\TestingFiles\file.csv");

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "1", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 1},
                    new Request{ ClientId = "2", RequestId = 1, Name = "Chleb", Price = 15M, Quantity = 2}
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
