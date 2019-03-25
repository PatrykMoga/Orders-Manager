using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Orders
{
    public class Product
    {
        public string Name { get; set; }        
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}
