using OrdersManager.ConsoleUI.MenuServiceComponents;
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
        public MenuItem Component { get; }

        public OrdersInPriceRange(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Orders in price range", Show);
        }

        private void Show()
        {
            Clear();
            WriteLine("Select filter for orders in price range\n");

            var filter = _filtersService.GetFilter();
            var min = Helper.ParseToDecimal("Enter minimum price: ");
            var max = Helper.ParseToDecimal("Enter maximum price: ");
            var requests = _requestProvider.RequestsInRangeWhere(filter.Filter, min, max);

            Clear();
            var searchPattern = filter.ContainsPattern ? _filtersService.SearchPattern : "";
            WriteLine($"Orders in price range \"{min:C2}-{max:C2}\" for \"{filter.Name}{searchPattern}\"\n");

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
                    Range = $"{min}-{max}"
                });
            }

            CsvSerializer.Serialize("a", "b", records);


            ReadLine();
        }
    }
}
