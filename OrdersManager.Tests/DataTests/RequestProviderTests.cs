using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Autofac.Extras.Moq;
using OrdersManager.Core.Repository;
using OrdersManager.Core.Data;
using System.Linq;

namespace OrdersManager.Tests.DataTests
{
    [TestFixture]
    public class RequestProviderTests
    {
        private Func<IRequest, bool> _getAll = r => true;
        private Func<IRequest, bool> _getClient1 = r => r.ClientId == "1";
        private Func<IRequest, bool> _getClient2 = r => r.ClientId == "2";

        [Test]
        public void Add_WhenCalled_AddRequestToRepository()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getAll))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>().GetWhere(_getAll);
                sut.Add(new Request());

                var actual = sut.Count;
                Assert.That(actual, Is.EqualTo(4));
            }
        }

        [Test]
        public void GetWhere_WhenCalled_ReturnsFilteredRequestList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getClient1))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>().GetWhere(_getClient1);

                var actual = sut[0].ClientId;
                Assert.That(actual, Is.EqualTo("1"));
            }
        }

        [Test]
        public void CountWhere_WhenCalled_ReturnOrdersCount()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getClient2))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>();

                var actual = sut.CountWhere(_getClient2);
                Assert.That(actual, Is.EqualTo(2));
            }
        }

        [Test]
        public void TotalAmountWhere_WhenCalled_GetTotalAmountOfOrders()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getAll))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>();

                var actual = sut.TotalAmountWhere(_getAll);
                Assert.That(actual, Is.EqualTo(160M));
            }
        }

        [Test]
        public void AverageAmountWhere_WhenCalled_GetAverageAmountOfOrders()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getAll))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>();

                var actual = sut.AverageAmountWhere(_getAll);
                Assert.That(actual, Is.EqualTo(80M));
            }
        }

        [Test]
        public void RequestsInRangeWhere_WhenCalled_GetRequestsInRange()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getAll))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>();

                var actual = sut.RequestsInRangeWhere(_getAll, 40, 60).Count;
                Assert.That(actual, Is.EqualTo(2));
            }
        }

        [Test]
        public void ProductRequestWhere_WhendCalled_GetProductsWithQuantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository>()
                    .Setup(r => r.GetWhere(_getAll))
                    .Returns(GetSampleRequests());

                var sut = mock.Create<RequestProvider>();

                var actual = sut.ProductRequestWhere(_getAll);

                var expected = new Dictionary<string, int>
                {
                    {"Rogal", 8  },
                    {"Bułka", 4  }
                };

                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.That(expected.Keys.ToList()[i] == actual.Keys.ToList()[i]);
                    Assert.That(expected.Values.ToList()[i] == actual.Values.ToList()[i]);
                }
            }

        }

        private IList<IRequest> GetSampleRequests()
        {
            return new List<IRequest>
            {
                new Request{ ClientId = "1", RequestId = 1, Name = "Rogal", Price = 20M, Quantity = 4},
                new Request{ ClientId = "2", RequestId = 1, Name = "Bułka", Price = 10M, Quantity = 4},
                new Request{ ClientId = "2", RequestId = 1, Name = "Rogal", Price = 10M, Quantity = 4}
            };
        }
    }
}
