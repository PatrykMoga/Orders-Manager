using Autofac.Extras.Moq;
using NUnit.Framework;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.FilesProcessing;
using OrdersManager.Core.Logs;
using System.Collections.Generic;

namespace OrdersManager.Tests.DeserializersTests
{
    [TestFixture]
    public class DeserializeServiceTests
    {
        private static readonly ILogger logger = new ConsoleLogger();

        private IEnumerable<string> files = new List<string>
        {
            @"..\..\..\TestingFiles\file.csv",
            @"..\..\..\TestingFiles\file.json",
            @"..\..\..\TestingFiles\file.xml"
        };

        private IEnumerable<IDeserializer> deserializers = new List<IDeserializer>
        {
            new CsvDeserializer(logger),
            new JsonDeserializer(logger),
            new XmlDeserializer(logger)
        };

        [Test]
        public void DeserializeFiles_Scenario_ExpectedBehavior()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFilesReader>()
                    .Setup(f => f.Files)
                    .Returns(files);

                mock.Provide(deserializers);

                var sut = mock.Create<DeserializeService>();
                var acutal = sut.DeserializeAllFiles();

                var expected = new List<IRequest>
                {
                    new Request{ ClientId = "1", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 1},
                    new Request{ ClientId = "2", RequestId = 1, Name = "Chleb", Price = 15M, Quantity = 2},
                    new Request{ ClientId = "3", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 6},
                    new Request{ ClientId = "4", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 7},
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
