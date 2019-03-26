using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersInPriceRange : IMenuComponent
    {
        public MenuItem Component { get; }

        public OrdersInPriceRange()
        {
            Component = new MenuItem("Orders in price range", Show);
        }

        private void Show()
        {
            
        }
    }
}
