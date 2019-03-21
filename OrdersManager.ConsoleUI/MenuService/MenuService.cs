using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuServiceComponents
{
    public class MenuService : IMenuService
    {
        private int _index = 1;
        private readonly IEnumerable<IMenuComponent> _menuComponents;
        private readonly Dictionary<int, MenuComponent> _executable;

        public MenuService(IEnumerable<IMenuComponent> menuComponents)
        {
            _menuComponents = menuComponents;
            _executable = new Dictionary<int, MenuComponent>();
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (var component in _menuComponents)
            {
                _executable.Add(_index++, component.Component);               
            }
        }

        public void PrintMenu()
        {          
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
            Clear();
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
                    WriteLine("Zła komenda, spróbuj pownownie!");
                    ReadKey();
                    Clear();
                }
            }
        }
    }
}
