using System.Collections.Generic;

namespace OrdersManager.Core.Filtering
{
    public interface IFilterProvider
    {
        string SerachPattern { get; }
        Dictionary<int, RequestFilter> GetFilters();
    }
}