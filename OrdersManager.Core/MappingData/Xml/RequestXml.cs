using OrdersManager.Core.Data;
using System.Xml.Serialization;

namespace OrdersManager.Core.MappingData.Xml
{
    [XmlRoot(ElementName = "request")]
    public class RequestXml : IRequest
    {
        [XmlElement(ElementName = "clientId")]
        public string ClientId { get; set; }
        [XmlElement(ElementName = "requestId")]
        public long? RequestId { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "quantity")]
        public int? Quantity { get; set; }
        [XmlElement(ElementName = "price")]
        public decimal? Price { get; set; }
    }
}
