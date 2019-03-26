﻿using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using OrdersManager.Core.Serializers;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class ProductsList : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        public MenuItem Component { get; }

        public ProductsList(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Products list", ShowOrders);
        }

        private void ShowOrders()
        {
            Clear();
            WriteLine("Products list\n");

            var filter = _filtersService.GetFilter();
            var requests = _requestProvider.ProductRequestWhere(filter.Filter);

            Clear();
            var searchPattern = filter.ContainsPattern ? _filtersService.SearchPattern : "";
            WriteLine($"Products list for \"{filter.Name}{searchPattern}\"\n");
            var titleRow = string.Format("{0,0} {1,12}",
                "Name", "Quantity");
            WriteLine(titleRow);


            WriteLine(titleRow.Length.PrintLines('-'));
            foreach (var request in requests)
            {
                var row = string.Format("{0,5} {1,8}",
                    request.Key, request.Value);
                WriteLine(row);
            }
            WriteLine(titleRow.Length.PrintLines('-'));

            var records = new List<object>();
            foreach (var item in requests)
            {
                records.Add(new { Name = item.Key, Quantity = item.Value });
            }
           
            CsvSerializer.Serialize("a", "b", records);
            ReadLine();
        }
    }
}