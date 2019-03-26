using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersAverage : IMenuComponent
    {
        public MenuItem Component { get; }

        public OrdersAverage()
        {
            Component = new MenuItem("Lista wszystkich zamówień", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
