using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class OrdersNumber : IMenuComponent
    {
        public MenuComponent Component { get; }

        public OrdersNumber()
        {
            Component = new MenuComponent("Lista wszystkich zamówień", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
