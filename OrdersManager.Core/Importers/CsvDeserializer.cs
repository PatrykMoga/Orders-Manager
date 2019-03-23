using CsvHelper;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrdersManager.Core.Importers
{
    public class CsvDeserializer : IDeserializer
    {
        private readonly ILogger _logger;

        public CsvDeserializer(ILogger logger)
        {
            _logger = logger;
        }

        public IList<Request> Deserialize(IEnumerable<string> files)
        {
            var requests = new List<Request>();

            foreach (var file in files.Where(f => f.EndsWith(".csv")))
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
