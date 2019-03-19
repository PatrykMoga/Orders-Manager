using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OrdersManager.Core.Importers
{
    public class DeserializeService
    {
        private readonly IEnumerable<IDeserializer> _deserializers;
        private readonly MemoryRepository _repository;

        public DeserializeService(IEnumerable<IDeserializer> deserializers, MemoryRepository repository)
        {
            _deserializers = deserializers;
            _repository = repository;
        }

        public void DeserializeAllFiles()
        {
            foreach (var deserializer in _deserializers)
            {
                deserializer.Deserialize().ToList()
                    .ForEach(r => _repository.Insert(r));
            }
        }
    }
}
