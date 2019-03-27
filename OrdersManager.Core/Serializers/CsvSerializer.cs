using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace OrdersManager.Core.Serializers
{
    public static class CsvSerializer
    {
        public static void Serialize(IEnumerable<object> records)
        {
            try
            {
                var file = ReadPath();
                using (var writer = new StreamWriter(file))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(records);
                }
                WriteLine($"File has been saved in: {Path.GetDirectoryName(file)} as {Path.GetFileName(file)}");
                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                ReadLine();
            }
        }

        private static string ReadPath()
        {
            WriteLine("Enter the directory path:");
            var path = ReadLine();
            WriteLine("Enter file name:");
            var name = ReadLine();
            return $"{path.Trim()}\\{name.Trim()}.csv";
        }
    }
}
