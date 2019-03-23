using System;
using System.Collections.Generic;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Repository
{
    public interface IRepository
    {
        IList<Request> GetAll();
        IList<Request> GetWhere(Func<Request, bool> filter);
        void Insert(Request order);
    }
}