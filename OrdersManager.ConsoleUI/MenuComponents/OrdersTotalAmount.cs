﻿using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using OrdersManager.Core.Serializers;
using OrdersManager.ConsoleUI.OptionsMenuComponents;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersTotalAmount : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        private readonly OptionsMenu Service;
        public MenuItem Component { get; }

        private decimal _amount;
        private string _filterName;

        public OrdersTotalAmount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Total Orders Amount", GenerateReport);
            Service = new OptionsMenu();

        }

        private void GenerateReport()
        {
            
            SetUp(out _amount, out _filterName);
            Print(_amount, _filterName);
        }

        private void SetUp(out decimal amount, out string filterName)
        {
            Clear();
            WriteLine("Select filter for total orders amount\n");
            var filterPattern = _filtersService.GetFilter();
            amount = _requestProvider.TotalAmountWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            filterName = filterPattern.Name + searchPattern;
        }

        private static void Print(decimal amount, string filterName)
        {
            Clear();
            WriteLine($"Total orders amount for \"{filterName}\": {amount:C2}");
            Serialize(amount, filterName);
        }

        private static void Serialize(decimal amount, string filterName)
        {
            var records = new List<object>();
            records.Add(new { TotalAmount = $"{amount:C2}", Filter = filterName });

            CsvSerializer.Serialize(records);
        }
    }
}
