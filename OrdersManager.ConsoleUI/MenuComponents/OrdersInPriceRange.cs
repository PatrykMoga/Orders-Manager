using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Extensions;
using OrdersManager.Core.Filtering;
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
            WriteLine("Orders in price range\n");

            var filter = _filtersService.GetFilter();
            //Console.WriteLine("Min");
            //var input = ReadLine();
            var min = 12;
            //Console.WriteLine("Max");
            //input = ReadLine();
            var max = 20;
            var requests = _requestProvider.RequestsInRangeWhere(filter.Filter, min, max);

            Clear();
            var searchPattern = filter.ContainsPattern ? _filtersService.SearchPattern : "";
            WriteLine($"Orders in price range \"{min}-{max}\" for \"{filter.Name}{searchPattern}\"\n");
            
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
    }
}
