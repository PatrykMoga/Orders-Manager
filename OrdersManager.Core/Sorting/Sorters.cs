using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersManager.Core.Sorting
{
    public static class Sorters
    {
        public static void OrderListBy(IList<IRequest> inValue, Func<IRequest, dynamic> sortingFilter,
            Action<IList<IRequest>> outValue, bool ascending)
        {
            outValue(inValue.OrderBy(sortingFilter).ToList());
            if (ascending)
            {
               
            }
            else
            {
                outValue(inValue.OrderByDescending(sortingFilter).ToList());
            }
        }
        //public static void OrderDictionaryBy(IList<(string, int)> inValue,
        //    Func<IList<(string, int)>,dynamic> filter, Action<IList<(string, int)>> outValue)
        //{           
        //    outValue(inValue.OrderByDescending(r => r.Item1).ToList());
        //}
    }
}
