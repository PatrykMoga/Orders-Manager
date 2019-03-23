using OrdersManager.Core.Requests;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Importers
{
    public class DeserializeService : IDeserializeService
    {
        private readonly IFilesReader _filesReader;
        private readonly IEnumerable<IDeserializer> _deserializers;

        public DeserializeService(IFilesReader filesReader, IEnumerable<IDeserializer> deserializers)
        {
            _filesReader = filesReader;
            _deserializers = deserializers;          
        }

        public IList<Request> DeserializeAllFiles()
        {
            var requests = new List<Request>();
            foreach (var deserializer in _deserializers)
            {
                deserializer.Deserialize(_filesReader.Files).ToList()
                    .ForEach(r => requests.Add(r));
            }
            return requests;
        }
    }
}
