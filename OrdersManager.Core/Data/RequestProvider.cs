using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;

namespace OrdersManager.Core.Data
{
    public class RequestProvider : IRequestProvider
    {
        private readonly IRepository _repository;

        public RequestProvider(IRepository repository)
        {
            _repository = repository;
        }      

        public void Add(IRequest request)
        {
            if (Validate(request))
            {
                _repository.Insert(request);
            }
        }

        public IList<IRequest> Get(Func<IRequest,bool> filter) => _repository.GetWhere(filter);

        private bool Validate(IRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId) || request.ClientId.Length > 6 || request.ClientId.Contains(" "))
                return false;

            if (request.RequestId == null)
                return false;

            if (request.Name == null || request.Name.Length > 255)
                return false;

            if (request.Price == null)
                return false;
            if (request.Quantity == null)
                return false;

            return true;
        }
    }
}
