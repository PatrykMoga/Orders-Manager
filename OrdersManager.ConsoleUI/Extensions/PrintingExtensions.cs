using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI.Extensions
{
    public static class PrintingExtensions
    {
        public static string PrintLines(this int length)
        {
            var sb = new StringBuilder();
            sb.Append('=', length);
            return sb.ToString();
        }

        public static string PrintInLines(this string str)
        {
            var sb = new StringBuilder();
            sb.Append('=', str.Length);
            sb.Append($"\n{str}\n");
            sb.Append('=', str.Length);
            return sb.ToString();
        }

        public static string PrintInLines(this string str, int max)
        {
            var sb = new StringBuilder();
            sb.Append('=', str.Length < max ? str.Length : max);
            sb.Append($"\n{str}\n");
            sb.Append('=', str.Length < max ? str.Length : max);
            return sb.ToString();
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
