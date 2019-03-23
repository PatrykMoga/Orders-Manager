using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OrdersManager.Core.Mapping;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Deserializers
{
    public class XmlDeserializer : IDeserializer
    {
        public string FileExtension => ".xml";

        public IList<IRequest> DeserializeFiles(IEnumerable<string> files)
        {
            var requests = new List<IRequest>();

            foreach (var file in files.Where(f => f.EndsWith(FileExtension)))
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
            return requests;
        }       
    }
}
