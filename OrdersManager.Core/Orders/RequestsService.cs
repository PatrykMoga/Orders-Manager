using OrdersManager.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersManager.Core.Orders
{
    public class RequestsService
    {       
        public IEnumerable<IRequest> Requests { get; set; }

        //public decimal TotalOrderPrice()
        //{
        //    var totatPrice = 0M;
        //    Requests.ToList().ForEach(p => totatPrice += (p.Price * p.Quantity));
        //    return totatPrice;
        //}


    }
}
