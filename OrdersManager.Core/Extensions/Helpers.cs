using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Extensions
{
    public static class Helpers
    {
        public static int ParseToInt(string input)
        {
            while (true)
            {
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Enter valid number");
                }
            }         
        }
    }
}
