using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Serializers;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.ConsoleUI.MenuItems
{
    public class OrdersTotalAmount : IMenuItem
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFilterService _filterService;
        private readonly OptionsMenu _optionsMenu;

        private decimal _amount;
        private string _filterName;

        public MenuItem MenuItem { get; }

        public OrdersTotalAmount(IRequestProvider requestProvider, IFilterService filterService)
        {
            _requestProvider = requestProvider;
            _filterService = filterService;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.AddItem(new MenuItem("Serialize report", () => Serialize(_amount, _filterName)));

            MenuItem = new MenuItem("Total Orders Amount", GenerateReport);
        }

        private void GenerateReport()
        {
            SetUp(out _amount, out _filterName);
            _optionsMenu.Return = false;
            while (!_optionsMenu.Return)
            {
                Print(_amount, _filterName);
            }
        }

        private void SetUp(out decimal amount, out string filterName)
        {
            Clear();
            WriteLine("Select filter for total orders amount\n");
            var filterPattern = _filterService.GetFilter();
            amount = _requestProvider.TotalAmountWhere(filterPattern.Filter);
            var searchPattern = filterPattern.ContainsPattern ? _filterService.SearchPattern : string.Empty;
            filterName = filterPattern.Name + searchPattern;
        }

        private void Print(decimal amount, string filterName)
        {
            Clear();
            WriteLine($"Total orders amount for \"{filterName}\": {amount:C2}");
            _optionsMenu.PrintMenu();
        }

        private void Serialize(decimal amount, string filterName)
        {
            var records = new List<object>
            {
                new { TotalAmount = $"{amount:C2}", Filter = filterName }
            };

            CsvSerializer.Serialize(records);
        }
    }
}
