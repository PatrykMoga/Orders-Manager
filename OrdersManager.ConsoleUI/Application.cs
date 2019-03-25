using OrdersManager.ConsoleUI.ApplicationComponents;
using OrdersManager.ConsoleUI.Extensions;
using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Logs;
using OrdersManager.Core.Repository;
using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI
{
    public class Application
    {
        private readonly IDataManager _manager;
        private readonly IMenuService _menuService;

        public Application(IMenuService menuService, IDataManager manager)
        {         
            _menuService = menuService;
            _manager = manager;
        }

        public void Start()
        {
            _manager.Initialize();
            Menu();
        }

        private void Menu()
        {
            while (true)
            {
                Clear();
                WriteLine("Wybierz z listy raport do wygenerowania:".PrintInLines());
                _menuService.PrintMenu();
            }
        }      
    }
}
