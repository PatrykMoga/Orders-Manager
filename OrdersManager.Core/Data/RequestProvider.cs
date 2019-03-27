using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (Valid(request))
            {
                _repository.Insert(request);
            }
        }

        private bool Valid(IRequest request)
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

        public IList<IRequest> GetWhere(Func<IRequest, bool> filter) => _repository.GetWhere(filter);

        private Dictionary<string, IEnumerable<(string name, int? quantity, decimal? price)>> OrdersWhere(Func<IRequest, bool> filter)
        {
            return _repository.GetWhere(filter)
                .GroupBy(r => $"{r.ClientId}-{r.RequestId}")
                .Select(r => new
                {
                    clientId = r.Key,
                    products = r.Select(p => new
                    {
                        name = p.Name,
                        quantity = p.Quantity,
                        price = p.Price
                    })
                })
                .OrderBy(r => r.clientId)
                .ToDictionary(r => r.clientId, r => r.products as IEnumerable<(string name, int? quantity, decimal? price)>);
        }

        public int CountWhere(Func<IRequest, bool> filter)
        {
            return _repository.GetWhere(filter)
                    .Select(r => $"{r.ClientId}-{r.RequestId}")
                    .Count();
        }

        public decimal TotalAmountWhere(Func<IRequest, bool> filter) =>
            (decimal)_repository.GetWhere(filter).Sum(r => r.Price * r.Quantity);

        public decimal AverageAmountWhere(Func<IRequest, bool> filter) => TotalAmountWhere(filter) / CountWhere(filter);

        public IList<IRequest> RequestsInRangeWhere(Func<IRequest, bool> filter, decimal min, decimal max)
        {
            return _repository.GetWhere(filter)
                    .Where(r => (r.Price * r.Quantity) >= min && (r.Price * r.Quantity) <= max)
                    .ToList();
        }

        public Dictionary<string, int> ProductRequestWhere(Func<IRequest, bool> filter)
        {
            return _repository.GetWhere(filter)
                    .GroupBy(r => r.Name)
                    .Select(r => new { name = r.Key, count = r.Sum(q => q.Quantity) })
                    .ToDictionary(r => r.name, r => (int)r.count);
        }
    }
}
