using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrdersManager.Core.MappingData.Json
{
    public class ListOfRequestsJson
    {
        [JsonProperty("requests")]
        public List<RequestJson> Requests { get; set; }
    }
}
