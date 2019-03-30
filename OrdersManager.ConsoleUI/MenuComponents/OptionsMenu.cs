using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OptionsMenu
    {
        private int _index = 1;
        private readonly Dictionary<int, MenuItem> _items;
        public bool Return { get; internal set; }

        public OptionsMenu()
        {
            _items = new Dictionary<int, MenuItem>();
        }

        public void AddItem(MenuItem item)
        {
            _items.Add(_index++, item);
        }

        public void AddRange(IList<MenuItem> items)
        {
            foreach (var item in items)
            {
                _items.Add(_index++, item);
            }
        }

        public void PrintMenu()
        {
            WriteLine();
            WriteLine("0: Return");

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
                    Return = true;
                    return;
                }
                else
                {
                    ExecuteMenuItem(input);
                    break;
                }
            }
        }

        private void ExecuteMenuItem(string actionKey)
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
                    ReadKey();
                }
            }
            else
            {
                WriteLine("Command error, try again!");
                ReadKey();
            }
        }
    }
}
