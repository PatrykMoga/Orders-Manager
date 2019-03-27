using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuItems
{
    public class OrdersCount : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        private readonly OptionsMenu _optionsMenu;

        private int _count;
        private string _filterName;

        public MenuItem Item { get; }

        public OrdersCount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_count, _filterName)));

            Item = new MenuItem("Orders count", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _count, out _filterName);
            _optionsMenu.Return = false;
            while (!_optionsMenu.Return)
            {
                Print(_count, _filterName);
            }
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
