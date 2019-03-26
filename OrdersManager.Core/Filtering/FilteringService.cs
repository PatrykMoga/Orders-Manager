using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace OrdersManager.Core.Filtering
{
    public class FilteringService : IFilteringService
    {
        private readonly IFilteringProvider _filteringProvider;

        public FilteringService(IFilteringProvider filteringProvider)
        {
            _filteringProvider = filteringProvider;
        }

        public void PrintFilters()
        {
            foreach (var filter in _filteringProvider.GetFilters())
            {
                WriteLine($"{filter.Key}: {filter.Value.Name}");
            }
            WriteLine();
        }

        public Func<IRequest, bool> GetFilter()
        {
            PrintFilters();
            while (true)
            {               
                Write("Enter command key: ");
                var filterKey = ReadLine();

                if (int.TryParse(filterKey, out int key))
                {
                    if (_filteringProvider.GetFilters().ContainsKey(key))
                    {
                        if (_filteringProvider.GetFilters()[key].ContainsPattern)
                        {
                            _filteringProvider.GetFilters()[key].ValidatePattern();
                            return _filteringProvider.GetFilters()[key].Filter;
                        }
                        return _filteringProvider.GetFilters()[key].Filter;
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
