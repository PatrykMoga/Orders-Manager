using System;
using System.Collections.Generic;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Repository
{
    public interface IRepository
    {
        IList<IRequest> GetAll();
        IList<IRequest> GetWhere(Func<IRequest, bool> filter);
        void Insert(IRequest order);
    }
}