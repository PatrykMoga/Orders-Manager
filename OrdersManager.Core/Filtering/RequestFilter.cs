using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Filtering
{
    public class RequestFilter
    {
        public string Name { get; set; }
        public Func<IRequest,bool> Filter { get; set; }

        public RequestFilter(string name, Func<IRequest, bool> filter)
        {
            Name = name;
            Filter = filter;
        }      
    }
}
