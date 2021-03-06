﻿using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuItems
{
    public class OrdersTotalAmount : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilterService _filterService;
        private readonly Report _report;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem MenuItem { get; }

        public OrdersTotalAmount(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;
            _report = new Report();
            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", Serialize));
            MenuItem = new MenuItem("Total Orders Amount", GenerateReport);
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
            WriteLine("Select filter for total orders amount\n");
            var filterPattern = _filterService.GetFilter();
            _report.Amount = _requestProvider.TotalAmountWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
            _report.FilteredBy = filterPattern.Name + searchPattern;
        }

        private void Print()
        {
            Clear();
            WriteLine($"Total orders amount for \"{_report.FilteredBy}\": {_report.Amount:C2}");
            _optionsMenu.PrintMenu();
        }

        private void Serialize()
        {
            var records = new List<object>
            {
                new { TotalAmount = $"{_report.Amount:C2}", _report.FilteredBy }
            };

            CsvSerializer.Serialize(records);
        }

        private class Report
        {
            public decimal Amount { get; set; }
            public string FilteredBy { get; set; }
        }
    }
}
