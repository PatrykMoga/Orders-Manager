using CsvHelper;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrdersManager.Core.Importers
{
    public class CsvDeserializer : IDeserializer
    {
        private readonly IFilesReader _reader;
        private readonly ILogger _logger;

        public CsvDeserializer(IFilesReader reader, ILogger logger)
        {
            _reader = reader;
            _logger = logger;
        }
        public IList<Request> Deserialize()
        {
            var requests = new List<Request>();

            foreach (var file in _reader.Files.Where(f => f.EndsWith(".csv")))
            {
                var message = $@"Plik: {file} został załadowany pomyślnie.";

                using (var streamReader = File.OpenText(file))
                using (var csvReader = new CsvReader(streamReader))
                {

                    csvReader.Configuration.RegisterClassMap<RequestCsvMap>();
                    while (csvReader.Read())
                    {
                        try
                        {
                            requests.Add(csvReader.GetRecord<Request>());

                        }
                        catch (Exception ex)
                        {                          
                            message = $"Plik: {file} zawiera błędne dane i zostały one zignorowane.\n";
                            message += $"Wiersz:{csvReader.Context.RawRow} Dane: {csvReader.Context.RawRecord}";
                        }
                    }
                }

                _logger.Log(message);
            }
            return requests;
        }
    }
}
