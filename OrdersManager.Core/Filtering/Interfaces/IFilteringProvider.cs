using System.Collections.Generic;

namespace OrdersManager.Core.Filtering
{
    public interface IFilteringProvider
    {
        Dictionary<int, RequestFilter> GetFilters();
    }
}