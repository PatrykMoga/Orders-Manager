using OrdersManager.Core.Data;
using OrdersManager.Core.Sorting;
using System.Collections.Generic;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OptionsProvider
    {
        public IList<MenuItem> RequestSortingOptions(IList<IRequest> requests)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Sort by client id",
                () =>  Sorters.OrderListBy(requests,r => r.ClientId,r => requests = r,true)),
                new MenuItem("Sort by request id",
                () =>  Sorters.OrderListBy(requests,r => r.RequestId,r => requests = r,true)),
                new MenuItem("Sort by name",
                () =>  Sorters.OrderListBy(requests,r => r.Name,r => requests = r,true)),
                new MenuItem("Sort by price",
                () =>  Sorters.OrderListBy(requests,r => r.Price,r => requests = r,true)),
                new MenuItem("Sort by quantity",
                () =>  Sorters.OrderListBy(requests,r => r.Quantity,r => requests = r,true)),
                new MenuItem("Sort by total price",
                () =>  Sorters.OrderListBy(requests,r => r.Price * r.Quantity,r => requests = r,true)),
            };
        }
    }
}
