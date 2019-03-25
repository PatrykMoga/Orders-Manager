using System;
using System.Text;

namespace OrdersManager.ConsoleUI
{
    public static class Extensions
    {
        public static void PrintLines(this int length)
        {
            var sb = new StringBuilder();
            sb.Append('=', length);
            Console.WriteLine(sb.ToString());
        }

        public static void PrintInLines(this string str)
        {
            var sb = new StringBuilder();
            sb.Append('=', str.Length);
            sb.Append($"\n{str}\n");
            sb.Append('=', str.Length);
            Console.WriteLine(sb.ToString());
        }

        public static void PrintInLines(this string str, int max)
        {
            var sb = new StringBuilder();
            sb.Append('=', str.Length < max ? str.Length : max);
            sb.Append($"\n{str}\n");
            sb.Append('=', str.Length < max ? str.Length : max);
            Console.WriteLine(sb.ToString());
        }
    }
}
