using System.Collections.Generic;

namespace OrdersManager.Core.Filtering
{
    public interface IFilteringProvider
    {
        string SerachPattern { get; }
        Dictionary<int, RequestFilter> GetFilters();
    }
}