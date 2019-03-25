using OrdersManager.Core.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OrdersManager.Core.Mapping
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
