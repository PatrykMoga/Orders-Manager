using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersManager.Core.Sorting
{
    public static class SortingService
    {
        public static IEnumerable<IRequest> SortByName(IEnumerable<IRequest> requests)
        {
            return requests.OrderBy(r => r.Name);
        }

        public static IEnumerable<IRequest> SortByNameDescending(IEnumerable<IRequest> requests)
        {
            return requests.OrderByDescending(r => r.Name);
        }

    }
}
