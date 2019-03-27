using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Extensions
{
    public static class Helper
    {
        public static decimal ParseToDecimal(string description)
        {
            while (true)
            {
                Console.Write(description);
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid number!");
                }
            }
        }
    }
}
