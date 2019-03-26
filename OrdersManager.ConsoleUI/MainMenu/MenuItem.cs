using System;

namespace OrdersManager.ConsoleUI.MenuServiceComponents
{
    public class MenuItem
    {
        public string Name { get; set; }
        public Action Action { get; set; }
        
        public MenuItem(string name, Action action )
        {
            Name = name;
            Action = action;
        }
    }
}