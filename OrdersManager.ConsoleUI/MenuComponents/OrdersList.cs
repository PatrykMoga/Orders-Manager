using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.ConsoleUI.OptionsMenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersList : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem Component { get; }

        private IOrderedEnumerable<IRequest> _requests;
        private string _filterName;

        public OrdersList(IRequestProvider requestProvider, IFilteringService filteringService)
        {
            _requestProvider = requestProvider;
            _filtersService = filteringService;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_requests, _filterName)));
            Component = new MenuItem("Orders List", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _requests, out _filterName);
            Print(_requests, _filterName);
        }

        private void SetUp(out IOrderedEnumerable<IRequest> requests, out string filterName)
        {
            Clear();
            WriteLine("Select filter for orders list\n");
            var filterPattern = _filtersService.GetFilter();
            requests = _requestProvider.GetWhere(filterPattern.Filter).OrderBy(r => r.ClientId).ThenBy(r => r.RequestId);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : string.Empty;
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(IOrderedEnumerable<IRequest> requests, string filterName)
        {
            Clear();
            WriteLine($"Orders List for \"{filterName}\"\n");
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
            _optionsMenu.PrintMenu();
        }

        private void Serialize(IOrderedEnumerable<IRequest> requests, string filterName)
        {
            var records = new List<object>();
            foreach (var request in requests)
            {
                records.Add(new
                {
                    request.RequestId,
                    request.ClientId,
                    request.Name,
                    request.Price,
                    request.Quantity,
                    Filter = filterName
                });
            }
            CsvSerializer.Serialize(records);
        }
    }
}
