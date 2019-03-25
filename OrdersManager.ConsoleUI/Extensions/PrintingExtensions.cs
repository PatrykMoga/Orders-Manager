using System;
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
    }
}
