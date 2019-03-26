using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Filtering
{
    public class RequestFilter
    {      
        public string Name { get; }
        public Func<IRequest,bool> Filter { get; }
        public bool ContainsPattern { get; }
        public Action ValidatePattern { get; }

        public RequestFilter(string name, Func<IRequest, bool> filter)
        {
            Name = name;
            Filter = filter;
        }

        public RequestFilter(string name, Func<IRequest, bool> filter, Action validatePattern)
        {
            Name = name;
            Filter = filter;
            ContainsPattern = true;
            ValidatePattern = validatePattern;
        }

    }
}
