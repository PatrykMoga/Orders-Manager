using OrdersManager.Core.Data;
using OrdersManager.Core.FilesProcessing;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Deserializers
{
    public class DeserializingService : IDeserializingService
    {
        private readonly IFilesReader _filesReader;
        private readonly IEnumerable<IDeserializer> _deserializers;

        public DeserializingService(IFilesReader filesReader, IEnumerable<IDeserializer> deserializers)
        {
            _filesReader = filesReader;
            _deserializers = deserializers;
        }

        public IList<IRequest> InitializeDeserializing()
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
