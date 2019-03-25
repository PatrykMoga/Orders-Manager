using OrdersManager.Core.Data;
using System;

namespace OrdersManager.Core.Filtering
{
    public static class RequestFilters
    {
        public static Func<IRequest, bool> GetAll() => (r => true);
        public static Func<IRequest, bool> GetByClientId(string clientId) => (r => r.ClientId == clientId);
    }
}
