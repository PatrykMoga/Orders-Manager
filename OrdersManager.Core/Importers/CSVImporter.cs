using CsvHelper;
using OrdersManager.Core.Orders;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;

namespace OrdersManager.Core.Importers
{
    public class CsvDeserializer : IDeserializer
    {
        private readonly MemoryRepository _repository;
        private readonly FilesReader _reader;

        public CsvDeserializer(MemoryRepository repository, FilesReader reader)
        {
            _repository = repository;
            _reader = reader;
        }
        public void Deserialize()
        {
            foreach (var file in _reader.Files.Where(f => f.EndsWith(".csv")))
            {
                using (var streamReader = File.OpenText(file))
                using (var reader = new CsvReader(streamReader))
                {
                    reader.Configuration.BadDataFound = context =>
                    {

                        Console.WriteLine($"Row:{context.RawRow},{context.RawRecord}");
                    };

                    reader.Configuration.RegisterClassMap<RequestMap>();
                    while (reader.Read() && !reader.Context.IsFieldBad)
                    {
                        var record = reader.GetRecord<Request>();
                        _repository.Insert(record);
                    }
                }
            }
        }
    }
}
