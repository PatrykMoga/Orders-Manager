using System;
using System.Collections.Generic;

namespace OrdersManager.Core.Data
{
    public interface IRequestProvider
    {
        void Add(IRequest request);
        IList<IRequest> GetWhere(Func<IRequest, bool> filter);
        int CountWhere(Func<IRequest, bool> filter);
        decimal TotalAmountWhere(Func<IRequest, bool> filter);
        decimal AverageAmountWhere(Func<IRequest, bool> filter);
        IList<IRequest> GetRequestsInRangeWhere(Func<IRequest, bool> filter, int min, int max);
        Dictionary<string, int> ProductRequestWhere(Func<IRequest, bool> filter);       
    }
}