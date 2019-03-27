using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.ConsoleUI.OptionsMenuComponents;
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
        private readonly OptionsMenu _optionsMenu;
        public MenuItem Component { get; }

        private decimal _average;
        private string _filterName;

        public OrdersAverageValue(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_average, _filterName)));
            Component = new MenuItem("Orders average value", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _average, out _filterName);
            Print(_average, _filterName);           
        }

        private void SetUp(out decimal average, out string filterName)
        {
            Clear();
            WriteLine("Select filter for orders average value\n");
            var filterPattern = _filtersService.GetFilter();
            average = _requestProvider.AverageAmountWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : string.Empty;
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(decimal average, string filterName)
        {
            Clear();
            WriteLine($"Average orders value for \"{filterName}\": {average:C2}");
            _optionsMenu.PrintMenu();
        }

        private void Serialize(decimal average, string filterName)
        {
            var records = new List<object>
            {
                new { Average = $"{average:C2}", Filter = $"{filterName}" }
            };

            CsvSerializer.Serialize(records);
        }        
    }
}
