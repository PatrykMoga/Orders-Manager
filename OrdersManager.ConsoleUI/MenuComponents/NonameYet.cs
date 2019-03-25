using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class NonameYet : IMenuComponent
    {
        private readonly IRequestProvider _provider;
        public MenuComponent Component { get; }

        public NonameYet(IRequestProvider provider)
        {
            _provider = provider;
            Component = new MenuComponent("Lista zamówień", ShowOrders);
        }

        private void ShowOrders()
        {
            Console.Clear();
            Console.WriteLine("Lista produktów");
            var filter = RequestFilters.GetAll();
            foreach (var item in _provider.ProductRequestWhere(filter))
            {
                //Console.WriteLine($"{item.Name} {item.ClientId} {item.RequestId} {item.Price} {item.Quantity}");
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Console.ReadLine();
        }
    }
}
