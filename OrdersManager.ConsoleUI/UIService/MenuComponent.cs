using System;

namespace OrdersManager.ConsoleUI.UIServiceComponents
{
    public class MenuComponent
    {
        public string Name { get; set; }
        public Action Action { get; set; }
        
        public MenuComponent(string name, Action action )
        {
            Name = name;
            Action = action;
        }
    }
}