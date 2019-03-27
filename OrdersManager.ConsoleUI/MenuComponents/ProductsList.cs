using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.ConsoleUI.OptionsMenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using OrdersManager.Core.Sorting;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class ProductsList : IMenuComponent
    {
        private readonly IFilteringService _filtersService;
        private readonly IRequestProvider _requestProvider;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem Component { get; }

        private Dictionary<string, int> _products;
        private string _filterName;

        public ProductsList(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;

            _optionsMenu = new OptionsMenu();
            LoadOptionsMenuItems();
            Component = new MenuItem("Products list", GenerateReport);
        }

        private void LoadOptionsMenuItems()
        {
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_products, _filterName)));

            _optionsMenu.AddItem(new MenuItem("Sort by name", () => SortingService.SortDictionaryByKey(ref _products)));
            _optionsMenu.AddItem(new MenuItem("Sort by name descending",
                () => SortingService.SortDictionaryByKeyDescending(ref _products)));

            _optionsMenu.AddItem(new MenuItem("Sort by quantity", () => SortingService.SortDictionaryByValue(ref _products)));
            _optionsMenu.AddItem(new MenuItem("Sort by quantity descending",
                () => SortingService.SortDictionaryByValueDescending(ref _products)));
        }

        private void GenerateReport()
        {
            SetUp(out _products, out _filterName);
            _optionsMenu.Return = false;
            while (!_optionsMenu.Return)
            {
                Print(_products, _filterName);
            }
        }

        private void SetUp(out Dictionary<string, int> products, out string filterName)
        {
            Clear();
            WriteLine("Select filter for products list\n");
            var filterPattern = _filtersService.GetFilter();
            products = _requestProvider.ProductRequestWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : string.Empty;
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(Dictionary<string, int> products, string filterName)
        {
            Clear();
            WriteLine($"Products list for \"{filterName}\"\n");
            var titleRow = string.Format("{0,0} {1,12}",
                "Name", "Quantity");
            WriteLine(titleRow);


            WriteLine(titleRow.Length.PrintLines('-'));
            foreach (var product in products)
            {
                var row = string.Format("{0,5} {1,8}",
                    product.Key, product.Value);
                WriteLine(row);
            }
            WriteLine(titleRow.Length.PrintLines('-'));
            _optionsMenu.PrintMenu();
        }

        private void Serialize(Dictionary<string, int> products, string filterName)
        {
            var records = new List<object>();
            foreach (var product in products)
            {
                records.Add(new
                {
                    Name = product.Key,
                    Quantity = product.Value,
                    Filter = filterName
                });
            }

            CsvSerializer.Serialize(records);
        }
    }
}
