using OrdersManager.Core.Requests;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Deserializers
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

        public IList<IRequest> DeserializeAllFiles()
        {
            var requests = new List<IRequest>();
            foreach (var deserializer in _deserializers)
            {
                deserializer.DeserializeFiles(_filesReader.Files).ToList()
                    .ForEach(r => requests.Add(r));
            }
            return requests;
        }
    }
}
