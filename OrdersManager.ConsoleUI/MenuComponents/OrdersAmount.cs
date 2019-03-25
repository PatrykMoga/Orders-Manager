using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class OrdersAmount : IMenuComponent
    {
        private readonly IRequestProvider _provider;
        public MenuComponent Component { get; }

        public OrdersAmount(IRequestProvider provider)
        {
            _provider = provider;
            Component = new MenuComponent("Ilość zamówień", bla);
        }

        private void bla()
        {
            //Console.WriteLine(_repository.GetAll().Count);
            Console.ReadLine();
        }
    }
}
