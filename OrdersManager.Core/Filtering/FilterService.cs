using static System.Console;

namespace OrdersManager.Core.Filtering
{
    public class FilterService : IFilterService
    {
        private readonly IFilterProvider _filterProvider;

        public FilterService(IFilterProvider filterProvider)
        {
            _filterProvider = filterProvider;
        }

        public string SearchPattern => _filterProvider.SerachPattern;

        private void PrintFilters()
        {
            foreach (var filter in _filterProvider.GetFilters())
            {
                WriteLine($"{filter.Key}: {filter.Value.Name}");
            }
            WriteLine();
        }

        public RequestFilter GetFilter()
        {
            PrintFilters();
            while (true)
            {
                Write("Enter command key: ");
                var filterKey = ReadLine();

                if (int.TryParse(filterKey, out int key))
                {
                    if (_filterProvider.GetFilters().ContainsKey(key))
                    {
                        if (_filterProvider.GetFilters()[key].ContainsPattern)
                        {
                            _filterProvider.GetFilters()[key].ValidatePattern();
                            return _filterProvider.GetFilters()[key];
                        }
                        return _filterProvider.GetFilters()[key];
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
