using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersCount : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        public MenuItem Component { get; }

        public OrdersCount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Orders count", Show);
        }

        private void Show()
        {
            Clear();
            WriteLine("Select filter for orders count\n");

            var filterPattern = _filtersService.GetFilter();
            var count = _requestProvider.CountWhere(filterPattern.Filter);

            Clear();
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            var filterName = filterPattern.Name + searchPattern;

            WriteLine($"Orders count for \"{filterName}\": {count}");

            var records = new List<object>();
            records.Add(new { Count = count, Filter = $"{filterPattern.Name}{searchPattern}" });

            CsvSerializer.Serialize(records);

            ReadLine();
        }
    }
}
