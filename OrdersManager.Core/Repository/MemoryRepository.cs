using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Repository
{
    public class MemoryRepository : IRepository
    {
        private readonly IList<IRequest> _requests;

        public MemoryRepository()
        {
            _requests = new List<IRequest>();     
        }

        public void Insert(IRequest order) => _requests.Add(order);

        public IList<IRequest> GetWhere(Func<IRequest, bool> filter) => _requests.Where(filter).ToList();

        public IList<IRequest> GetAll() => _requests;     
    }
}
