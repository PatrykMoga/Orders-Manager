using System;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.Filtering
{
    public interface IFilteringService
    {
        string SearchPattern { get; }
        RequestFilter GetFilter();
    }
}