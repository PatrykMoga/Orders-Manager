using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersAverageValue : IMenuComponent
    {
        public MenuItem Component { get; }

        public OrdersAverageValue()
        {
            Component = new MenuItem("Orders average value", Show);
        }

        private void Show()
        {
            throw new NotImplementedException();
        }
    }
}
