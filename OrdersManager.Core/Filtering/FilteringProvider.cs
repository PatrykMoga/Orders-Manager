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

        private void ValidateClientId()
        {
            while (true)
            {
                WriteLine(string.Join(" ",_repository.GetWhere(r => true).Select(r => r.Name)));
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

        public void PrintFilters()
        {
            foreach (var filter in _filters)
            {
                WriteLine($"{filter.Key}: {filter.Value.Name}");
            }
        }

        public Func<IRequest, bool> GetFilter()
        {
            while (true)
            {
                var filterKey = ReadLine();
                if (int.TryParse(filterKey, out int key))
                {
                    if (_filters.ContainsKey(key))
                    {
                        if (_filters[key].ContainsPattern)
                        {
                            _filters[key].ValidatePattern();
                            return _filters[key].Filter;
                        }
                        return _filters[key].Filter;
                    }
                    else
                    {
                        WriteLine("Unknown command, try again!");
                    }
                }
                else
                {
                    WriteLine("Command error, try again!");
                }
            }
        }
    }
}
