using System.Collections.Generic;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Deserializers
{
    public interface IDeserializeService
    {
        IList<IRequest> DeserializeAllFiles();
    }
}