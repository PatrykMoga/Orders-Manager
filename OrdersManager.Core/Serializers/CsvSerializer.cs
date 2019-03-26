using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrdersManager.Core.Serializers
{
    public class CsvSerializer
    {
        public void Serialize(string dirPath, string fileName, IEnumerable<object> records)
        {
            var shit = $@"{dirPath}\{fileName}.csv";
            try
            {
                using (var writer = new StreamWriter(@"d:\testfolder\ser\file1.csv"))
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
