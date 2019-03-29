using System.Text;

namespace OrdersManager.Core.Extensions
{
    public static class Print
    {
        public static string PrintLines(this int length, char c)
        {
            var sb = new StringBuilder();
            sb.Append(c, length);
            return sb.ToString();
        }

        public static string PrintInLines(this string str, char c)
        {
            var sb = new StringBuilder();
            sb.Append(c, str.Length);
            sb.Append($"\n{str}\n");
            sb.Append(c, str.Length);
            return sb.ToString();
        }
    }
}
