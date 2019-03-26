using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

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
            WriteLine("Orders List\n");

            var filter = _filtersService.GetFilter();
            var requests = _requestProvider.ProductRequestWhere(filter);

            Clear();
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

            ReadLine();
        }
    }
}
