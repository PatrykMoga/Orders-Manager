using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OrdersManager.Core.Orders
{
    public class Request
    {
        public string ClientId { get; set; }
        public long RequestId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
