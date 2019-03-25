using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class OrdersInRange : IMenuComponent
    {
        public MenuComponent Component { get; }

        public OrdersInRange()
        {
            Component = new MenuComponent("Lista wszystkich zamówień", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
