﻿using OrdersManager.ConsoleUI.MenuServiceComponents;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.OptionsMenuComponents
{
    public class OptionsMenu
    {
        private int _index = 1;
        private readonly Dictionary<int, MenuItem> _items;

        public OptionsMenu()
        {
            _items = new Dictionary<int, MenuItem>();
        }

        public void AddItem(MenuItem item)
        {
            _items.Add(_index++, item);
        }

        public void PrintMenu()
        {
            WriteLine();
            WriteLine("0: Return");
            while (true)
            {
                foreach (var item in _items)
                {
                    WriteLine($"{item.Key}: {item.Value.Name}");
                }
                WriteLine();

                while (true)
                {
                    Write("Enter command key: ");
                    var input = ReadLine();
                    if (input == "0")
                    {
                        return;
                    }
                    ExecuteComponent(input);
                }
            }
        }

        public void ExecuteComponent(string actionKey)
        {
            if (int.TryParse(actionKey, out int key))
            {
                if (_items.ContainsKey(key))
                {
                    _items[key].Action();
                }
                else
                {
                    WriteLine("Unknown command, try again!");
                }
            }
            else
            {
                WriteLine("Command error, try again!");
            }
        }
    }
}
