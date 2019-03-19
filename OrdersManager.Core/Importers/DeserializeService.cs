using OrdersManager.Core.Orders;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Importers
{
    public class DeserializeService : IDeserializeService
    {
        private readonly IEnumerable<IDeserializer> _deserializers;

        public DeserializeService(IEnumerable<IDeserializer> deserializers)
        {
            _deserializers = deserializers;
        }

        public IList<Request> DeserializeAllFiles()
        {
            var list = new List<Request>();
            foreach (var deserializer in _deserializers)
            {
                deserializer.Deserialize().ToList()
                    .ForEach(r => list.Add(r));
            }
            return list;
        }
    }
}
