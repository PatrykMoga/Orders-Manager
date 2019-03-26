using OrdersManager.Core.Data;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace OrdersManager.Core.Filtering
{
    public class FilteringProvider : IFilteringProvider
    {
        private string _searchPattern;
        private readonly IRepository _repository;
        private readonly Dictionary<int, RequestFilter> _filters;

        public FilteringProvider(IRepository repository)
        {
            _repository = repository;

            _filters = new Dictionary<int, RequestFilter>();
            _filters.Add(1, new RequestFilter("All", r => true));
            _filters.Add(2, new RequestFilter("Cliend Id", r => r.ClientId == _searchPattern, ValidateClientId));
        }

        public Dictionary<int, RequestFilter> GetFilters() => _filters;

        private IEnumerable<string> ClientIds => _repository.GetAll().Select(r => r.ClientId).Distinct();

        private void ValidateClientId()
        {
            while (true)
            {
                Console.WriteLine("Available client Ids:");
                WriteLine(string.Join(", ",ClientIds));
                Write("Enter Client Id: ");
                var clientId = ReadLine();
                if (_repository.Contains(r => r.ClientId == clientId))
                {
                    _searchPattern = clientId;
                    break;
                }
                else
                {
                    WriteLine("Client doesn't exist");
                }
            }
        }       
    }
}
