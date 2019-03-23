using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrdersManager.Core.Mapping;
using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
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
                var obj = (ListOfRequestsJson)serializer.Deserialize(streamReader, typeof(ListOfRequestsJson));
                requests.AddRange(obj.Requests);
            }
            return requests;
        }
    }
}
