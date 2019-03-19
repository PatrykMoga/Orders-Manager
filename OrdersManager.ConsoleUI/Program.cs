using OrdersManager.Core;
using OrdersManager.Core.Importers;
using OrdersManager.Core.Orders;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersManager.ConsoleUI
{
    internal static class Program
    {
        private static void Main()
        {
            //string dir = @"D:\TestFolder\Inner";
            //var reader = new FilesReader();
            //reader.ReadFiles(dir, System.IO.SearchOption.AllDirectories);

            //foreach (var item in reader.Files)
            //{
            //    Console.WriteLine(item);
            //}
            //var repo = new MemoryRepository();
            //var csv = new CsvDeserializer(repo, reader);
            //try
            //{
            //    csv.Deserialize();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            //foreach (var item in repo.GetAll())
            //{
            //    Console.WriteLine($"{item.Name} {item.ClientId} {item.Price} {item.Quantity} {item.RequestId}");
           

        }
    }
}
