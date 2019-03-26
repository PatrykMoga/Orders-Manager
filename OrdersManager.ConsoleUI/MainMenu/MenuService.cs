using System;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuServiceComponents
{
    public class MenuService : IMenuService
    {
        private int _index = 1;
        private readonly IEnumerable<IMenuComponent> _menuComponents;
        private readonly Dictionary<int, MenuItem> _executable;

        public MenuService(IEnumerable<IMenuComponent> menuComponents)
        {
            _menuComponents = menuComponents;
            _executable = new Dictionary<int, MenuItem>();
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (var component in _menuComponents)
            {
                _executable.Add(_index++, component.Component);
            }
            _executable.Add(_index++, new MenuItem("Exit", ExitApp));
        }

        private void ExitApp()
        {
            Environment.Exit(0);
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
