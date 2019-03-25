using System;
using System.Collections.Generic;

namespace OrdersManager.Core.Data
{
    public interface IRequestProvider
    {
        void Add(IRequest request);
        IList<IRequest> Get(Func<IRequest, bool> filter);
    }
}