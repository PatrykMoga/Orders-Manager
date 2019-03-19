using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.UIServiceComponents
{
    public class UIService : IUIService
    {
        private int _index = 1;
        private readonly IEnumerable<IUIComponent> _uiComponents;
        private readonly Dictionary<int, UIComponent> _executable;
        private readonly List<UIComponent> _actions;

        public UIService(IEnumerable<IUIComponent> uiComponents)
        {
            _uiComponents = uiComponents;
            _executable = new Dictionary<int, UIComponent>();
            _actions = new List<UIComponent>();
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (var component in _uiComponents)
            {
                if (!component.Component.Executable)
                {
                    _actions.Add(component.Component);
                }
                if (component.Component.Executable)
                {
                    _executable.Add(_index++, component.Component);
                }
            }
        }

        public void Run()
        {
            foreach (var item in _actions)
            {
                item.Action();
            }
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
