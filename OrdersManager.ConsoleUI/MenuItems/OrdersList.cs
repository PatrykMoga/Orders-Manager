using OrdersManager.ConsoleUI.ApplicationComponents;
using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuItems
{
    public class OrdersList : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilterService _filterService;
        private readonly Report _report;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem MenuItem { get; }

        public OrdersList(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;
            _report = new Report();
            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddRange(SortingOptions());
            MenuItem = new MenuItem("Orders List", GenerateReport);
        }

        private IList<MenuItem> SortingOptions()
        {
            return new List<MenuItem>()
            {
                new MenuItem("Serialize report", Serialize),
                new MenuItem("Sort by client id",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.ClientId,r =>  _report.Requests = r)),
                new MenuItem("Sort by request id",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.RequestId,r =>  _report.Requests = r)),
                new MenuItem("Sort by name",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.Name,r =>  _report.Requests = r)),
                new MenuItem("Sort by price",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.Price,r =>  _report.Requests = r)),
                new MenuItem("Sort by quantity",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.Quantity,r =>  _report.Requests = r)),
                new MenuItem("Sort by total price",
                () =>  Sorter.OrderListBy( _report.Requests,r => r.Price * r.Quantity,r =>  _report.Requests = r)),
            };
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
            WriteLine("Select filter for orders list\n");
            var filterPattern = _filterService.GetFilter();
            _report.Requests = _requestProvider.GetWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
            _report.FilteredBy = filterPattern.Name + searchPattern;
        }

        private void Print()
        {
            Clear();
            WriteLine($"Orders List for \"{_report.FilteredBy}\"\n");
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
                    _report.FilteredBy
                });
            }

            CsvSerializer.Serialize(records);
        }

        private class Report
        {
            public IList<IRequest> Requests { get; set; }
            public string FilteredBy { get; set; }
        }
    }
}
