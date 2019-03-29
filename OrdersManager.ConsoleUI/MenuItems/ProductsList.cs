﻿using OrdersManager.ConsoleUI.MenuComponents;
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
        private readonly OptionsMenu _optionsMenu;
        public MenuItem MenuItem { get; }

        private Dictionary<string, int> _products;
        private string _filterName;

        public ProductsList(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;

            _optionsMenu = new OptionsMenu();
            LoadOptionsMenuItems();
            MenuItem = new MenuItem("Products list", GenerateReport);
        }

        private void LoadOptionsMenuItems()
        {
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_products, _filterName)));

            _optionsMenu.AddItem(new MenuItem("Sort by name", () => SortingProvider.SortDictionaryByKey(ref _products)));
            _optionsMenu.AddItem(new MenuItem("Sort by name descending",
                () => SortingProvider.SortDictionaryByKeyDescending(ref _products)));

            _optionsMenu.AddItem(new MenuItem("Sort by quantity", () => SortingProvider.SortDictionaryByValue(ref _products)));
            _optionsMenu.AddItem(new MenuItem("Sort by quantity descending",
                () => SortingProvider.SortDictionaryByValueDescending(ref _products)));
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
            var filterPattern = _filterService.GetFilter();
            products = _requestProvider.ProductRequestWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
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
