using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersAverageValue : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        public MenuItem Component { get; }

        public OrdersAverageValue(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Orders average value", Show);
        }

        private void Show()
        {
            Clear();
            WriteLine("Select filter for orders average value\n");

            var filterPattern = _filtersService.GetFilter();
            var average = _requestProvider.AverageAmountWhere(filterPattern.Filter);

            Clear();
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            var filterName = filterPattern.Name + searchPattern;

            WriteLine($"Average orders value for \"{filterName}\": {average:C2}");

            var records = new List<object>();
            records.Add(new { Average = $"{average:C2}", Filter = $"{filterPattern.Name}{searchPattern}" });

            CsvSerializer.Serialize(records);

            ReadLine();
        }
    }
}
