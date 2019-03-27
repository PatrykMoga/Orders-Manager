using OrdersManager.ConsoleUI.MenuServiceComponents;
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
        public MenuItem Component { get; }

        public OrdersList(IRequestProvider requestProvider, IFilteringService filteringService)
        {
            _requestProvider = requestProvider;
            _filtersService = filteringService;

            Component = new MenuItem("Orders List", ShowOrders);
        }
       
        private void ShowOrders()
        {
            Clear();
            WriteLine("Select filter for orders list\n");
            
            var filterPattern = _filtersService.GetFilter();
            var requests = _requestProvider.GetWhere(filterPattern.Filter).OrderBy(r => r.ClientId).ThenBy(r => r.RequestId);

            Clear();
            var searchPattern = filterPattern.ContainsPattern ? _filtersService.SearchPattern : "";
            var filterName = filterPattern.Name + searchPattern;

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
                });
            }
            CsvSerializer.Serialize(records);

            ReadLine();
        }
    }
}
