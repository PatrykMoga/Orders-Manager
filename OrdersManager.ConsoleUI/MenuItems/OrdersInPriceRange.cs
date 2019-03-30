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
    public class OrdersInPriceRange : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilterService _filterService;
        private readonly Report _report;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem MenuItem { get; }

        public OrdersInPriceRange(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;
            _report = new Report();
            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", Serialize));

            MenuItem = new MenuItem("Orders in price range", GenerateReport);
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
            WriteLine("Select filter for orders in price range\n");

            var filterPattern = _filterService.GetFilter();
            _report.MinPrice = Parse.ParseToDecimal("Enter minimum price: ");
            _report.MaxPrice = Parse.ParseToDecimal("Enter maximum price: ");
            _report.Requests = _requestProvider.RequestsInRangeWhere(filterPattern.Filter, _report.MinPrice, _report.MaxPrice);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
            _report.FilteredBy = filterPattern.Name + searchPattern;
        }

        private void Print()
        {
            Clear();
            WriteLine($"Orders in price range \"{_report.MinPrice:C2}-{_report.MaxPrice:C2}\" for \"{_report.FilteredBy}\"\n");

            if (_report.Requests.Count > 0)
            {
                var titleRow = string.Format("{0,0} {1,0} {2,5} {3,8} {4,10} {5,15}",
                "RequestId", "ClientId", "Name", "Price", "Quantity", "Total Price");
                WriteLine(titleRow);
                WriteLine(titleRow.Length.PrintLines('-'));
                foreach (var request in _report.Requests)
                {
                    var row = string.Format("{0,5} {1,8} {2,10} {3,8:C2} {4,5} {5,18:C2}",
                        request.RequestId, request.ClientId, request.Name,
                        request.Price, request.Quantity, request.Price * request.Quantity);
                    WriteLine(row);
                }
                WriteLine(titleRow.Length.PrintLines('-'));
                _optionsMenu.PrintMenu();
            }
            else
            {
                WriteLine("No orders for the customer in this price range");
                _optionsMenu.Return = true;
                ReadKey();
            }
        }

        private void Serialize()
        {
            var records = new List<object>();
            foreach (var request in _report.Requests)
            {
                records.Add(new
                {
                    request.RequestId,
                    request.ClientId,
                    request.Name,
                    request.Price,
                    request.Quantity,
                    TotalPrice = request.Price * request.Quantity,
                    _report.FilteredBy,
                    PriceRange = $"{_report.MinPrice}-{_report.MaxPrice}"
                });
            }

            CsvSerializer.Serialize(records);
        }

        private class Report
        {
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
            public IList<IRequest> Requests { get; set; }
            public string FilteredBy { get; set; }
        }
    }
}
