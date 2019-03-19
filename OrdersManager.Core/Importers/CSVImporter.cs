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
            var exceptions = new List<Exception>();

            foreach (var file in _reader.Files.Where(f => f.EndsWith(".csv")))
            {
                using (var streamReader = File.OpenText(file))
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.RegisterClassMap<RequestCsvMap>();
                    while (csvReader.Read())
                    {
                        try
                        {
                            var record = csvReader.GetRecord<Request>();
                            _repository.Insert(record);
                        }
                        catch (Exception)
                        {
                            var message = $"Plik: {file} zawiera błędne dane i zostały one zignorowane.\nWiersz:{csvReader.Context.RawRow} {csvReader.Context.RawRecord}";
                            exceptions.Add(new InvalidDataException(message));
                        }
                    }
                }
            }

            foreach (var ex in exceptions)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
