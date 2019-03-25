using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersCount : IMenuComponent
    {
        private readonly IRequestProvider _provider;
        public MenuComponent Component { get; }

        public OrdersCount(IRequestProvider provider)
        {
            _provider = provider;
            Component = new MenuComponent("Ilość zamówień", bla);
        }

        private void bla()
        {
            var filter = RequestFilters.GetAll();
            Console.WriteLine(_provider.CountWhere(filter));
            Console.ReadLine();
        }
    }
}
