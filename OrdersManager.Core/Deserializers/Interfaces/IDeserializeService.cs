using OrdersManager.Core.Data;
using System.Collections.Generic;

namespace OrdersManager.Core.Deserializers
{
    public interface IDeserializeService
    {
        IList<IRequest> DeserializeAllFiles();
    }
}