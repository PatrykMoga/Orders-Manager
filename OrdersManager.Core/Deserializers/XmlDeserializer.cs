using OrdersManager.Core.Logs;
using OrdersManager.Core.MappingData.Xml;
using OrdersManager.Core.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OrdersManager.Core.Deserializers
{
    public class XmlDeserializer : IDeserializer
    {
        private readonly ILogger _logger;

        public XmlDeserializer(ILogger logger)
        {
            _logger = logger;
        }

        public IList<IRequest> DeserializeFiles(IEnumerable<string> files)
        {
            var requests = new List<IRequest>();

            foreach (var file in files.Where(f => f.EndsWith(".xml")))
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
                var serializer = new XmlSerializer(typeof(ListOfRequestsXml));
                var r = (ListOfRequestsXml)serializer.Deserialize(streamReader);
               requests.AddRange(r.Requests);
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
