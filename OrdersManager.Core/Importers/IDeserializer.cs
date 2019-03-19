using OrdersManager.Core.Orders;
using System.Collections.Generic;

namespace OrdersManager.Core.Importers
{
    public interface IDeserializer
    {
        IList<Request> Deserialize();
    }
}