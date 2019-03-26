using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuComponents
{
    public class OrdersCount : IMenuComponent
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilteringService _filtersService;
        public MenuItem Component { get; }

        public OrdersCount(IRequestProvider requestProvider, IFilteringService filtersService)
        {
            _requestProvider = requestProvider;
            _filtersService = filtersService;
            Component = new MenuItem("Orders count", Show);
        }

        private void Show()
        {
            Clear();
            WriteLine("Orders count\n");

            var filter = _filtersService.GetFilter();
            var count = _requestProvider.CountWhere(filter.Filter);

            Clear();
            var searchPattern = filter.ContainsPattern ? _filtersService.SearchPattern : "";
            WriteLine($"Orders count for \"{filter.Name}{searchPattern}\": {count}");

            ReadLine();
        }
    }
}
