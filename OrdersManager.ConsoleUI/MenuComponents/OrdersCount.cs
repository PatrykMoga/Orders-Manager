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
        public MenuItem Component { get; }
        private readonly MenuService menuService;

        public OrdersCount(IRequestProvider provider)
        {
            _provider = provider;
            Component = new MenuItem("Orders count", Show);
        }

        private void Show()
        {
            //var filter = RequestFilters.GetAll();
            //Console.WriteLine(_provider.CountWhere(filter));
            //Console.ReadLine();
        }
    }
}
