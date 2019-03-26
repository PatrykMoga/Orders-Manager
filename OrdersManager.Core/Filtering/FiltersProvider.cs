using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace OrdersManager.Core.Filtering
{
    public class FiltersProvider
    {
        private int _index = 1;
        private readonly Dictionary<int, RequestFilter> _filters;

        public FiltersProvider()
        {
            _filters = new Dictionary<int, RequestFilter>();
            //_items.Add(_index++, new RequestFilter("All", r => true));
            //_items.Add(_index++, new RequestFilter("Cliend Id", r => r.ClientId == ReadCliendId()));
        }
        
        public void AddFilter(RequestFilter filter)
        {
            _filters.Add(_index++, filter);
        }

        public void PrintFilters()
        {          
            foreach (var filter in _filters)
            {
                WriteLine($"{filter.Key}: {filter.Value.Name}");
            }

            //while (true)
            //{
            //    var input = ReadLine();
            //    ExecuteComponent(input);
            //    break;
            //}
        }

        public Func<IRequest,bool> GetFilter(string filterKey)
        {            
            while (true)
            {
                if (int.TryParse(filterKey, out int key))
                {
                    if (_filters.ContainsKey(key))
                    {
                        return _filters[key].Filter;
                    }
                    else
                    {
                        WriteLine("Unknown command, try again!");
                        ReadKey();
                        Clear();
                    }
                }
                else
                {
                    WriteLine("Command error, try again!");
                    ReadKey();
                    Clear();
                }
            }          
        }
    }
}
