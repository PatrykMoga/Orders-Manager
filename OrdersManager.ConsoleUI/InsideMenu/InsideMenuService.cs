using OrdersManager.ConsoleUI.MenuServiceComponents;
using System;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.InsideMenu
{
    public class InsideMenuService
    {
        private int _index = 1;
        private readonly Dictionary<int, MenuItem> _items;

        public InsideMenuService()
        {
            _items = new Dictionary<int, MenuItem>();
        }

        public void AddItem(MenuItem item)
        {
            _items.Add(_index++, item);
        }

        public void PrintMenu()
        {          
            foreach (var item in _items)
            {
                WriteLine($"{item.Key}: {item.Value.Name}");
            }
            WriteLine();

            while (true)
            {
                var input = ReadLine();
                ExecuteComponent(input);
                break;
            }
        }

        public void ExecuteComponent(string actionKey)
        {          
            if (int.TryParse(actionKey, out int key))
            {
                if (_items.ContainsKey(key))
                {
                    
                }
                else
                {
                    WriteLine("Unknown command, try again!");
                    ReadKey();
                    Clear();
                }
            }
            else
            {
                WriteLine("Command error, try again!");
                ReadKey();
                Clear();
            }
        }
    }
}
