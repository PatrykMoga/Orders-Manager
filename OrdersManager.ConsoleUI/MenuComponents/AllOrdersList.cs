using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class AllOrdersList : IMenuComponent
    {
        private readonly IRepository _repository;
        public MenuComponent Component { get; }

        public AllOrdersList(IRepository repository)
        {
            _repository = repository;
            Component = new MenuComponent("Lista wszystkich zamówień", ShowOrders);
        }

        private void ShowOrders()
        {
            foreach (var item in _repository.GetWhere(c => true))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
