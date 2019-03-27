using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;

namespace OrdersManager.Core.Repository
{
    public interface IRepository
    {
        IList<IRequest> GetAll();
        IList<IRequest> GetWhere(Func<IRequest, bool> filter);
        void Insert(IRequest order);
        bool Contains(Func<IRequest, bool> filter);
    }
}