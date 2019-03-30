using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using OrdersManager.Core.Sorting;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuItems
{
    public class ProductsList : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilterService _filterService;
        private readonly Report _report;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem MenuItem { get; }       

        public ProductsList(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;
            _report = new Report();
            _optionsMenu = new OptionsMenu();
            LoadOptionsMenuItems();
            MenuItem = new MenuItem("Products list", GenerateReport);
        }

        private void LoadOptionsMenuItems()
        {
            _optionsMenu.AddItem(new MenuItem("Serialize report", Serialize));

            //_optionsMenu.AddItem(new MenuItem("Sort by name", () => SortingProvider.SortDictionaryByKey(ref _products)));
            //_optionsMenu.AddItem(new MenuItem("Sort by name descending",
            //    () => SortingProvider.SortDictionaryByKeyDescending(ref _products)));

            //_optionsMenu.AddItem(new MenuItem("Sort by quantity", () => SortingProvider.SortDictionaryByValue(ref _products)));
            //_optionsMenu.AddItem(new MenuItem("Sort by quantity descending",
            //    () => SortingProvider.SortDictionaryByValueDescending(ref _products)));
        }

        private void GenerateReport()
        {
            SetUp();
            _optionsMenu.Return = false;
            while (!_optionsMenu.Return)
            {
                Print();
            }
        }

        private void SetUp()
        {
            Clear();
            WriteLine("Select filter for products list\n");
            var filterPattern = _filterService.GetFilter();
            _report.Products = _requestProvider.ProductRequestWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
            _report.FilteredBy = filterPattern.Name + searchPattern;
        }

        private void Print()
        {
            Clear();
            WriteLine($"Products list for \"{_report.FilteredBy}\"\n");
            var titleRow = string.Format("{0,0} {1,12}",
                "Name", "Quantity");
            WriteLine(titleRow);

            WriteLine(titleRow.Length.PrintLines('-'));
            foreach (var product in _report.Products)
            {
                var row = string.Format("{0,5} {1,8}",
                    product.Key, product.Value);
                WriteLine(row);
            }
            WriteLine(titleRow.Length.PrintLines('-'));
            _optionsMenu.PrintMenu();
        }

        private void Serialize()
        {
            var records = new List<object>();
            foreach (var product in _report.Products)
            {
                records.Add(new
                {
                    Name = product.Key,
                    Quantity = product.Value,
                    _report.FilteredBy
                });
            }

            CsvSerializer.Serialize(records);
        }

        private class Report
        {
            public Dictionary<string,int> Products { get; set; }
            public string FilteredBy { get; set; }
        }
    }
}
