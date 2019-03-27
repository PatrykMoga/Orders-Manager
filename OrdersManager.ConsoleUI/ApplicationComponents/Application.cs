using OrdersManager.ConsoleUI.MenuComponents;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public class Application : IApplication
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMainMenu _mainMenu;

        public Application(IDataProvider dataProvider, IMainMenu mainMenu)
        {
            _dataProvider = dataProvider;
            _mainMenu = mainMenu;
        }

        public void Start()
        {
            _dataProvider.Initialize();
            while (true)
            {
                _mainMenu.PrintMenu();
            }
        }
    }
}
