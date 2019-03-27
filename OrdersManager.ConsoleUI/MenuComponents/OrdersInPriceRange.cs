using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.ConsoleUI.OptionsMenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;


namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersInPriceRange : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        private readonly OptionsMenu _optionsMenu;
        public MenuItem Component { get; }

        private decimal _min;
        private decimal _max;
        private IList<IRequest> _requests;
        private string _filterName;

        public OrdersInPriceRange(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_min, _max, _requests, _filterName)));
            Component = new MenuItem("Orders in price range", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _min, out _max, out _requests, out _filterName);
            Print(_min, _max, _requests, _filterName);
            while (true)
            {
                _optionsMenu.PrintMenu();
            }
            
        }

        private void SetUp(out decimal min, out decimal max, out IList<IRequest> requests, out string filterName)
        {
            Clear();
            WriteLine("Select filter for orders in price range\n");

            var filterPattern = _filtersService.GetFilter();
            min = Helper.ParseToDecimal("Enter minimum price: ");
            max = Helper.ParseToDecimal("Enter maximum price: ");
            requests = _requestProvider.RequestsInRangeWhere(filterPattern.Filter, min, max);
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(decimal min, decimal max, IList<IRequest> requests, string filterName)
        {
            Clear();
            WriteLine($"Orders in price range \"{min:C2}-{max:C2}\" for \"{filterName}\"\n");

            if (requests.Count > 0)
            {
                var titleRow = string.Format("{0,0} {1,0} {2,5} {3,8} {4,10} {5,15}",
                "RequestId", "ClientId", "Name", "Price", "Quantity", "Total Price");
                WriteLine(titleRow);
                WriteLine(titleRow.Length.PrintLines('-'));
                foreach (var request in requests)
                {
                    var row = string.Format("{0,5} {1,8} {2,10} {3,8:C2} {4,5} {5,15:C2}",
                        request.RequestId, request.ClientId, request.Name,
                        request.Price, request.Quantity, request.Price * request.Quantity);
                    WriteLine(row);
                }
                WriteLine(titleRow.Length.PrintLines('-'));
            }
            else
            {
                WriteLine("No orders for the customer in this price range");
            }
            ReadLine();
        }

        private void Serialize(decimal min, decimal max, IList<IRequest> requests, string filterName)
        {
            var records = new List<object>();
            foreach (var request in requests)
            {
                records.Add(new
                {
                    RequestId = request.RequestId,
                    ClientId = request.ClientId,
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    TotalPrice = request.Price * request.Quantity,
                    Filter = filterName,
                    Range = $"{min}-{max}"
                });
            }

            CsvSerializer.Serialize(records);
        }
    }
}
