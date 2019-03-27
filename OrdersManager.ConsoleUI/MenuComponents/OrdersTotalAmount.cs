using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using OrdersManager.Core.Serializers;
using OrdersManager.ConsoleUI.InsideMenu;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersTotalAmount : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        private readonly InsideMenuService Service;
        public MenuItem Component { get; }

        public OrdersTotalAmount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Total Orders Amount", Show);
            Service = new InsideMenuService();
            //Service.AddItem(new MenuItem("Serialize", () => Serialize()));
        }

        private void Show()
        {
            Clear();
            WriteLine("Select filter for total orders amount\n");

            var filterPattern = _filtersService.GetFilter();
            var amount = _requestProvider.TotalAmountWhere(filterPattern.Filter);

            Clear();
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            var filterName = filterPattern.Name + searchPattern;
            WriteLine($"Total orders amount for \"{filterName}\": {amount:C2}");
            Serialize(amount, filterName);

            ReadLine();
        }

        private static void Serialize(decimal amount, string filterName)
        {
            var records = new List<object>();
            records.Add(new { TotalAmount = $"{amount:C2}", Filter = filterName });

            CsvSerializer.Serialize("a", "b", records);
        }
    }
}
