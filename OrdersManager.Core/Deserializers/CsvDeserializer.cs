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

            using (var streamReader = File.OpenText(file))
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.Configuration.RegisterClassMap<RequestCsvMap>();
                while (csvReader.Read())
                {
                    try
                    {
                        var request = csvReader.GetRecord<Request>();
                        if (request != null)
                        {
                            requests.Add(request);
                        }    
                    }
                    catch (Exception)
                    {
                        _logger.LogError($"Plik: {file} zawiera uszkodzone dane w wierszu {csvReader.Context.RawRow}: \"{csvReader.Context.RawRecord.TrimEnd()}\"");
                    }
                }
            }
            if (requests.Count > 0)
            {
                _logger.LogSuccess($"Plik: {file} został załadowany.");
            }
            else
            {
                _logger.LogError($"Plik: {file} nie zawierał żadnych dancyh do załadowania.");
            }

            return requests;
        }
    }
}
