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
    public class OrdersTotalAmount : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        public MenuItem Component { get; }

        public OrdersTotalAmount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Total Orders Amount", Show);
        }

        private void Show()
        {
            Clear();
            WriteLine("Total Orders Amount\n");

            var filter = _filtersService.GetFilter();
            var amount = _requestProvider.TotalAmountWhere(filter.Filter);

            Clear();
            var searchPattern = filter.ContainsPattern ? _filtersService.SearchPattern : "";
            WriteLine($"Total orders amount for \"{filter.Name}{searchPattern}\": {amount:C2}");

            ReadLine();
        }
    }
}
