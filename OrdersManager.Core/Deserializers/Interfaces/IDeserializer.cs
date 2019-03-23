using OrdersManager.Core.Requests;
using System.Collections.Generic;

namespace OrdersManager.Core.Deserializers
{
    public interface IDeserializer
    {
        IList<IRequest> DeserializeFiles(IEnumerable<string> files);
        IList<IRequest> DeserializeFile(string file);
    }
}