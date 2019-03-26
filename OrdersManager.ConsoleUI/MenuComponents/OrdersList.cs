﻿using OrdersManager.ConsoleUI.Extensions;
using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersList : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringProvider _filtersProvider;
        public MenuItem Component { get; }

        public OrdersList(IRequestProvider requestProvider, IFilteringProvider filteringProvider)
        {
            _requestProvider = requestProvider;
            _filtersProvider = filteringProvider;
            Component = new MenuItem("Orders List", ShowOrders);
        }
       
        private void ShowOrders()
        {
            Clear();
            WriteLine("Orders List\n");

            _filtersProvider.PrintFilters();
            var filter = _filtersProvider.GetFilter();
            var requests = _requestProvider.GetWhere(filter);


            var titleRow = string.Format("{0,0} {1,0} {2,5} {3,8} {4,10}",
                "RequestId", "ClientId", "Name", "Price", "Quantity");
            WriteLine(titleRow);

            WriteLine(titleRow.Length.PrintLines('-'));
            foreach (var request in requests)
            {
                var row = string.Format("{0,5} {1,8} {2,10} {3,8:C2} {4,5}",
                    request.RequestId, request.ClientId, request.Name, request.Price, request.Quantity);
                WriteLine(row);
            }
            WriteLine(titleRow.Length.PrintLines('-'));

            ReadLine();
        }
    }
}
