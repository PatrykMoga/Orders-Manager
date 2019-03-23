using OrdersManager.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace OrdersManager.Core.Repository
{
    public class MemoryRepository : IRepository
    {
        private readonly IList<Request> _requests;

        public MemoryRepository()
        {
            _requests = new List<Request>
            {
                new Request{ ClientId = "1", Name = "Name1", Price = 5M, Quantity = 3, RequestId = 1},
                new Request{ ClientId = "2", Name = "Name2", Price = 355M, Quantity = 3, RequestId = 2},
                new Request{ ClientId = "2", Name = "Name3", Price = 15M, Quantity = 3, RequestId = 3}
            };
        }

        public void Insert(Request order)
        {
            _requests.Add(order);
        }

        public IList<Request> GetWhere(Func<Request,bool> filter)
        {
            return _requests.Where(filter).ToList();
        }

        public IList<Request> GetAll() => _requests;

        private bool Validate(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId) || request.ClientId.Length > 6 || request.ClientId.Contains(" "))
            {
                return false;
            }
            if (request.RequestId == null)
            {
                return false;
            }

            if (true)
            {

            }
            return true;
        }
    }
}
