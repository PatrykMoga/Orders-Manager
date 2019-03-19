using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Importers
{
    public class DeserializeService
    {
        private readonly IEnumerable<IDeserializer> _deserializers;

        public DeserializeService(IEnumerable<IDeserializer> deserializers)
        {
            _deserializers = deserializers;
        }

        public void DeserializeAllFiles()
        {
            foreach (var deserializer in _deserializers)
            {
                deserializer.Deserialize();
            }
        }
    }
}
