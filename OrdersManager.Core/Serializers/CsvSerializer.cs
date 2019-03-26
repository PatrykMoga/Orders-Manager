using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrdersManager.Core.Serializers
{
    public static class CsvSerializer
    {
        private static int index = 1;
        public static void Serialize(string dirPath, string fileName, IEnumerable<object> records)
        {
            var shit = $@"{dirPath}\{fileName}.csv";
            try
            {
                using (var writer = new StreamWriter($@"d:\testfolder\ser\file{index++}.csv"))
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
    }
}
