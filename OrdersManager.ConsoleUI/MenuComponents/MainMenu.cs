using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class MainMenu : IMainMenu
    {
        private int _index = 1;
        private readonly IEnumerable<IMenuItem> _menuItems;
        private readonly Dictionary<int, MenuItem> _executableItems;

        public MainMenu(IEnumerable<IMenuItem> menuItems)
        {
            _menuItems = menuItems;
            _executableItems = new Dictionary<int, MenuItem>();
            LoadItems();
        }

        private void LoadItems()
        {
            foreach (var item in _menuItems)
            {
                _executableItems.Add(_index++, item.MenuItem);
            }
        }

        public void PrintMenu()
        {
            Clear();
            WriteLine("Select the report to generate:\n");
            foreach (var item in _executableItems)
            {
                WriteLine($"{item.Key}: {item.Value.Name}");
            }
            WriteLine();

            while (true)
            {
                var input = ReadLine();
                ExecuteMenuItem(input);
                break;
            }
        }

        private void ExecuteMenuItem(string actionKey)
        {
            if (int.TryParse(actionKey, out int key))
            {
                if (_executableItems.ContainsKey(key))
                {
                    _executableItems[key].Action();
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
