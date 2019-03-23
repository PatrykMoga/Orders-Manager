using CsvHelper;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrdersManager.Core.Deserializers
{
    public class CsvDeserializer : IDeserializer
    {
        private readonly ILogger _logger;

        public CsvDeserializer(ILogger logger)
        {
            _logger = logger;
        }

        public IList<IRequest> DeserializeFiles(IEnumerable<string> files)
        {
            var requests = new List<IRequest>();

            foreach (var file in files.Where(f => f.EndsWith(".csv")))
            {
                requests.AddRange(DeserializeFile(file));
            }
            return requests;
        }

        public IList<IRequest> DeserializeFile(string file)
        {
            var requests = new List<IRequest>();
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
                    catch (Exception)
                    {
                        message = $"Plik: {file} zawiera błędne dane i zostały one zignorowane.\n";
                        message += $"Wiersz:{csvReader.Context.RawRow} Dane: {csvReader.Context.RawRecord}";
                    }
                }
            }
            _logger.Log(message);
            return requests;
        }
    }
}
