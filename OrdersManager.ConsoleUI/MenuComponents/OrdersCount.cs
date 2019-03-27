using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.ConsoleUI.OptionsMenuComponents;
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
        private readonly OptionsMenu _optionsMenu;
        public MenuItem Component { get; }

        private int _count;
        private string _filterName;

        public OrdersCount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_count, _filterName)));
            Component = new MenuItem("Orders count", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _count, out _filterName);
            Print(_count, _filterName);
        }

        private void SetUp(out int count, out string filterName)
        {
            Clear();
            WriteLine("Select filter for orders count\n");
            var filterPattern = _filtersService.GetFilter();
            count = _requestProvider.CountWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : string.Empty;
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(int count, string filterName)
        {
            Clear();
            WriteLine($"Orders count for \"{filterName}\": {count}");
            _optionsMenu.PrintMenu();
        }

        private void Serialize(int count, string filterName)
        {
            var records = new List<object>
            {
                new { Count = count, Filter = $"{filterName}" }
            };

            CsvSerializer.Serialize(records);
        }
    }
}
