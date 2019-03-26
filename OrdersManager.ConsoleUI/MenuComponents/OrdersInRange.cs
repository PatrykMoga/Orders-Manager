using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersInRange : IMenuComponent
    {
        public MenuItem Component { get; }

        public OrdersInRange()
        {
            Component = new MenuItem("Lista wszystkich zamówień", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
