using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.UIComponents
{
    public class OrdersAmount : IMenuComponent
    {
        private readonly IRepository _repository;
        public MenuComponent Component { get; }

        public OrdersAmount(IRepository repository)
        {
            _repository = repository;
            Component = new MenuComponent("Wypisz ilość zamówieć", bla);
        }

        private void bla()
        {
            Console.WriteLine(_repository.GetAll().Count);
            Console.ReadLine();
        }
    }
}
