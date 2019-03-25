using OrdersManager.ConsoleUI.Extensions;
using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class AllOrdersList : IMenuComponent
    {
        private readonly IRequestProvider _provider;
        public MenuComponent Component { get; }

        public AllOrdersList(IRequestProvider provider)
        {
            _provider = provider;
            Component = new MenuComponent("Lista zamówień", ShowOrders);
        }

        private void ShowOrders()
        {
            Console.Clear();
            Console.WriteLine("Lista zamówień".PrintInLines());
            var filter = RequestFilters.GetByClientId("1");
            foreach (var item in _provider.Get(filter))
            {
                Console.WriteLine($"{item.Name} {item.ClientId} {item.RequestId} {item.Price}");               
            }
            Console.ReadLine();
        }
    }
}
