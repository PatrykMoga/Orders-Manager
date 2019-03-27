using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace OrdersManager.Core.Serializers
{
    public static class CsvSerializer
    {
        private static int index = 1;

        public static void Serialize(IEnumerable<object> records)
        {
            try
            {
                var file = ReadFileDirectory();
                using (var writer = new StreamWriter(file))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string ReadFileDirectory()
        {
            return $@"d:\testfolder\ser\file{index++}.csv";
        }
    }
}
