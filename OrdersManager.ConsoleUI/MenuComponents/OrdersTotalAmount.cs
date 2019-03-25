using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersTotalAmount : IMenuComponent
    {
        private readonly IRequestProvider _provider;
        public MenuComponent Component { get; }

        public OrdersTotalAmount(IRequestProvider provider)
        {
            Component = new MenuComponent("Średnia wartość zamówienia", Show);
            _provider = provider;
        }

        private void Show()
        {
            //var filter = RequestFilters.GetAll();
            var filter = RequestFilters.GetByClientId("4");
            Console.WriteLine($"{_provider.TotalAmountWhere(filter):C2}");
            Console.ReadLine();
        }
    }
}
