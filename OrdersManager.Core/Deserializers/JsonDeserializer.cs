using Newtonsoft.Json;
using OrdersManager.Core.Logs;
using OrdersManager.Core.MappingData.Json;
using OrdersManager.Core.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrdersManager.Core.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
        private readonly ILogger _logger;

        public JsonDeserializer(ILogger logger)
        {
            _logger = logger;
        }

        public IList<IRequest> DeserializeFiles(IEnumerable<string> files)
        {
            var requests = new List<IRequest>();

            foreach (var file in files.Where(f => f.EndsWith(".json")))
            {
                requests.AddRange(DeserializeFile(file));
            }
            return requests;
        }

        public IList<IRequest> DeserializeFile(string file)
        {
            var requests = new List<IRequest>();
            using (var streamReader = File.OpenText(file))
            {
                var serializer = new JsonSerializer();
                try
                {
                    var obj = (ListOfRequestsJson)serializer.Deserialize(streamReader, typeof(ListOfRequestsJson));
                    requests.AddRange(obj.Requests);
                }
                catch (System.Exception)
                {
                    _logger.LogError($"Plik: {file} jest uszkodzony.");
                }               
            }

            if (requests.Count > 0)
            {
                _logger.LogSuccess($"File: {file} has been loaded.");
            }
            else
            {
                _logger.LogError($"File: {file} did not contain any data to be loaded.");
            }
            return requests;
        }
    }
}
