using OrdersManager.Core.Requests;
using System.Collections.Generic;

namespace OrdersManager.Core.Importers
{
    public interface IDeserializer
    {
        IList<Request> Deserialize();
    }
}