using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrdersManager.Core.Mapping
{
    public class ListOfRequestsJson
    {
        public List<RequestJson> Requests { get; set; }
    }
}
