using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class OrdersAverage : IMenuComponent
    {
        public MenuComponent Component { get; }

        public OrdersAverage()
        {
            Component = new MenuComponent("Lista wszystkich zamówień", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
