using OrdersManager.ConsoleUI.ApplicationComponents;
using OrdersManager.ConsoleUI.MenuServiceComponents;

namespace OrdersManager.ConsoleUI
{
    public class Application
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMenuService _menuService;

        public Application(IMenuService menuService, IDataProvider dataProvider)
        {
            _menuService = menuService;
            _dataProvider = dataProvider;
        }

        public void Start()
        {
            _dataProvider.Initialize();
            while (true)
            {
                _menuService.PrintMenu();
            }
        }
    }
}
