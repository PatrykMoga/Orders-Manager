using System;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.Filtering
{
    public interface IFilterService
    {
        string SearchPattern { get; }
        RequestFilter GetFilter();
    }
}