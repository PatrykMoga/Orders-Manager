using OrdersManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public static class Sorter
    {
        public static void OrderListBy(IList<IRequest> inValue, Func<IRequest, dynamic> sortingFilter,
            Action<IList<IRequest>> outValue)
        {
            SortBy(out bool ascending);
            if (ascending)
            {
                outValue(inValue.OrderBy(sortingFilter).ToList());
            }
            else
            {
                outValue(inValue.OrderByDescending(sortingFilter).ToList());
            }
        }

        public static void OrderDictionaryByKey(Dictionary<string, int> inValue,
            Action<Dictionary<string, int>> outValue)
        {
            SortBy(out bool ascending);
            if (ascending)
            {
                outValue(inValue.OrderBy(r => r.Key).ToDictionary(x => x.Key, x => x.Value));
            }
            else
            {
                outValue(inValue.OrderByDescending(r => r.Key).ToDictionary(x => x.Key, x => x.Value));
            }
        }

        public static void OrderDictionaryByValue(Dictionary<string, int> inValue,
            Action<Dictionary<string, int>> outValue)
        {
            SortBy(out bool ascending);
            if (ascending)
            {
                outValue(inValue.OrderBy(r => r.Value).ToDictionary(x => x.Key, x => x.Value));
            }
            else
            {
                outValue(inValue.OrderByDescending(r => r.Value).ToDictionary(x => x.Key, x => x.Value));
            }
        }

        private static void SortBy(out bool ascending)
        {
            WriteLine("1: Sort ascending");
            WriteLine("2: Sort descending");
            while (true)
            {
                Write("Enter command key: ");
                var sorting = ReadLine();

                if (sorting == "1")
                {
                    ascending = true;
                    return;
                }
                if (sorting == "2")
                {
                    ascending = false;
                    return;
                }
                else
                {
                    WriteLine("Command error, try again!");
                }
            }
        }
    }
}
