using System;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.Filtering
{
    public interface IFilteringService
    {
        Func<IRequest, bool> GetFilter();
        void PrintFilters();
    }
}