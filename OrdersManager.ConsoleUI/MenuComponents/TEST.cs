using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class TEST : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        public MenuItem Component { get; }
      
        public TEST(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
            Component = new MenuItem("TEST", Show);
        }

        private void Show()
        {
            foreach (var item in _requestProvider.OrdersWhere(r => true))
            {
                Console.WriteLine(item.Key + item.Value);
                foreach (var f in item.Value)
                {
                    Console.WriteLine(f.price);
                }
                
            }
            Console.ReadLine();
        }
    }
}
