using System.Xml.Serialization;

namespace OrdersManager.Core.MappingData.Xml
{
    [XmlRoot("requests")]
    public class ListOfRequestsXml
    {
        [XmlElement("request")]
        public RequestXml[] Requests { get; set; }
    }
}
