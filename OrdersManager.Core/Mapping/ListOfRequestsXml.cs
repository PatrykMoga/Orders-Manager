using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OrdersManager.Core.Mapping
{
    [XmlRoot("requests")]
    public class ListOfRequestsXml
    {
        [XmlElement("request")]
        public RequestXmlMap[] Requests { get; set; }
    }
}
