using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class MainMenu : IMainMenu
    {
        private int _index = 1;
        private readonly IEnumerable<IMenuItem> _menuItems;
        private readonly Dictionary<int, MenuItem> _executable;

        public MainMenu(IEnumerable<IMenuItem> menuItems)
        {
            _menuItems = menuItems;
            _executable = new Dictionary<int, MenuItem>();
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (var component in _menuItems)
            {
                _executable.Add(_index++, component.Item);
            }
        }

        public void PrintMenu()
        {
            Clear();
            WriteLine("Select the report to generate:\n");
            foreach (var item in _executable)
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

        private void ExecuteComponent(string actionKey)
        {
            if (int.TryParse(actionKey, out int key))
            {
                if (_executable.ContainsKey(key))
                {
                    _executable[key].Action();
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
