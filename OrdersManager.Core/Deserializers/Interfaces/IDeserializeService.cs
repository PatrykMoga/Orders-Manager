using System.Collections.Generic;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.Deserializers
{
    public interface IDeserializeService
    {
        IList<IRequest> DeserializeAllFiles();
    }
}