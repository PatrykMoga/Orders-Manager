using OrdersManager.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.Core.Sorting
{
    public static class SortingProvider
    {
        public static void SortListByName(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.Name).ToList();
        }

        public static void SortListByNameDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.Name).ToList();
        }

        public static void SortListByClientId(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.ClientId).ToList();
        }

        public static void SortListByClientIdDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.ClientId).ToList();
        }

        public static void SortListByRequestId(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.RequestId).ToList();
        }

        public static void SortListByRequestIdDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.RequestId).ToList();
        }

        public static void SortListByQuantity(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.Quantity).ToList();
        }

        public static void SortListByQuantityDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.Quantity).ToList();
        }

        public static void SortListByPrice(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.Price).ToList();
        }

        public static void SortListByPriceDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.Price).ToList();
        }

        public static void SortListByTotalPrice(ref IList<IRequest> requests)
        {
            requests = requests.OrderBy(r => r.Price * r.Quantity).ToList();
        }

        public static void SortListByTotalPriceDescending(ref IList<IRequest> requests)
        {
            requests = requests.OrderByDescending(r => r.Price * r.Quantity).ToList();
        }

        public static void SortDictionaryByKey(ref Dictionary<string, int> valuePairs)
        {
            valuePairs = valuePairs.OrderBy(r => r.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void SortDictionaryByKeyDescending(ref Dictionary<string, int> valuePairs)
        {
            valuePairs = valuePairs.OrderByDescending(r => r.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void SortDictionaryByValue(ref Dictionary<string, int> valuePairs)
        {
            valuePairs = valuePairs.OrderBy(r => r.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void SortDictionaryByValueDescending(ref Dictionary<string, int> valuePairs)
        {
            valuePairs = valuePairs.OrderByDescending(r => r.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
