using OrdersManager.Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersManager.Core.Repository
{
    public class MemoryRepository
    {
        private readonly IList<Order> _orders;

        public MemoryRepository()
        {
            _orders = new List<Order>
            {
                new Order{ ClientId = "1", Name = "Name1", Price = 5M, Quantity = 3, RequestId = 1},
                new Order{ ClientId = "2", Name = "Name2", Price = 355M, Quantity = 3, RequestId = 2},
                new Order{ ClientId = "2", Name = "Name3", Price = 15M, Quantity = 3, RequestId = 3}
            };
        }

        public void Insert(Order order) => _orders.Add(order);

        public IList<Order> GetWhere(Func<Order,bool> filter)
        {
            return _orders.Where(filter).ToList();
        }

        public IList<Order> GetAll()
        {
            return GetWhere(c => true);
        }
    }
}
