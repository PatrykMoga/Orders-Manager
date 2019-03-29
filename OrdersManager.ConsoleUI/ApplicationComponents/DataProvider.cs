using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.FilesProcessing;
using OrdersManager.Core.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public class DataProvider : IDataProvider
    {
        private readonly IFilesReader _filesReader;
        private readonly IDeserializingService _deserializeService;
        private readonly IRequestProvider _provider;
        private readonly ILogger _logger;

        public DataProvider(IFilesReader filesReader, IDeserializingService deserializeService,
            IRequestProvider provider, ILogger logger)
        {
            _filesReader = filesReader;
            _deserializeService = deserializeService;
            _provider = provider;
            _logger = logger;
        }

        public void GetData()
        {
            ReadData();
            var requests = _deserializeService.InitializeDeserializing();
            PrintLogs();
            SaveToMemory(requests);
        }

        private void ReadData()
        {
            while (true)
            {
                try
                {
                    Clear();
                    WriteLine("To begin, enter the directory path that contains the files to be processed.\n" +
                        $"Supported files extensions: \"{string.Join(", ", _filesReader.SupportedExtensions)}\".\n");
                    Write("Path: ");
                    var dirPath = ReadLine();
                    _filesReader.ReadFiles(dirPath, SearchOption.AllDirectories);
                    if (!_filesReader.Files.Any())
                    {
                        WriteLine("The directory did not contain any supported files. Try again or choose different directory.");
                        ReadKey();
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                    ReadKey();
                }
            }
        }

        private void PrintLogs()
        {
            Clear();
            WriteLine("Logs for all found files:");
            _logger.PrintLogs();
            WriteLine("Press any key to continue.");
            ReadKey();
        }

        private void SaveToMemory(IList<IRequest> requests)
        {
            requests.ToList().ForEach(r => _provider.Add(r));
        }
    }
}
