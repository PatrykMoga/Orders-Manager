using System.Collections.Generic;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Importers
{
    public interface IDeserializeService
    {
        IList<Request> DeserializeAllFiles();
    }
}