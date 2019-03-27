using Newtonsoft.Json;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.MappingData.Json
{
    public class RequestJson : IRequest
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("requestId")]
        public long? RequestId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }
    }
}
