using System;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.Filtering
{
    public interface IFilteringProvider
    {
        Func<IRequest, bool> GetFilter();
        void PrintFilters();
    }
}