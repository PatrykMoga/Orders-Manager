using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
            return (IList<IRequest>)JsonConvert.DeserializeObject<List<RequestJson>>(file);
        }
    }
}
