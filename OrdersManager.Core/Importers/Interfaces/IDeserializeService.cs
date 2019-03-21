using System.Collections.Generic;
using OrdersManager.Core.Orders;

namespace OrdersManager.Core.Importers
{
    public interface IDeserializeService
    {
        IList<Request> DeserializeAllFiles();
    }
}