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
        private readonly IList<IRequest> _requests;

        public MemoryRepository()
        {
            _requests = new List<IRequest>();
           
        }

        public void Insert(IRequest order)
        {
            _requests.Add(order);
        }

        public IList<IRequest> GetWhere(Func<IRequest,bool> filter)
        {
            return _requests.Where(filter).ToList();
        }

        public IList<IRequest> GetAll() => _requests;

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
