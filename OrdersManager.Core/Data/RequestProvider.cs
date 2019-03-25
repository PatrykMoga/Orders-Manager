using OrdersManager.Core.Extensions;
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
            if (Validate(request))
            {
                _repository.Insert(request);
            }
        }

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

        public IList<IRequest> GetWhere(Func<IRequest, bool> filter) => _repository.GetWhere(filter);

        //public int CountWhere(Func<IRequest, bool> filter) =>
        //    _repository.GetWhere(filter).
        //    DistinctBy(r => $"{r.ClientId}-{r.RequestId}")
        //    .Count();

        public int CountWhere(Func<IRequest, bool> filter) =>
            _repository.GetWhere(filter).Select(r => $"{r.ClientId}-{r.RequestId}").Distinct().Count();

        public decimal TotalAmountWhere(Func<IRequest, bool> filter) =>
            (decimal)_repository.GetWhere(filter).Select(r => r.Price * r.Quantity).Sum();

        public decimal AverageAmountWhere(Func<IRequest, bool> filter) =>
            (decimal)_repository.GetWhere(filter).Select(r => r.Price * r.Quantity).Average();

        public IList<IRequest> GetRequestsInRangeWhere(Func<IRequest, bool> filter, int min, int max) =>
            _repository.GetWhere(filter).Where(r => (r.Price * r.Quantity) > min && (r.Price * r.Quantity) < max).ToList();

        public Dictionary<string, int> ProductRequestWhere(Func<IRequest, bool> filter)
        {
            //var cut = from r in _repository.GetWhere(filter)
            //          group r by r.Name into g
            //          select new
            //          {
            //              name = g.Key,
            //              count = (from r in g select r.Name).Distinct().Count()
            //          };

            var cut = _repository.GetWhere(filter).GroupBy(r => r.Name)
                .Select(r => new { name = r.Key, count = r.Count() })
                .ToDictionary(r => r.name, r => r.count);


            return cut;
        }
    }
}
