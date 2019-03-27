using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.FilesProcessing;
using OrdersManager.Core.Logs;
using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public class DataProvider : IDataProvider
    {
        private readonly IFilesReader _filesReader;
        private readonly IDeserializeService _deserializeService;
        private readonly IRequestProvider _provider;
        private readonly ILogger _logger;

        public DataProvider(IFilesReader filesReader, IDeserializeService deserializeService,
            IRequestProvider provider, ILogger logger)
        {
            _filesReader = filesReader;
            _deserializeService = deserializeService;
            _provider = provider;
            _logger = logger;
        }

        public void Initialize()
        {
            LoadDirectory();
            DeserializeAndSave();
        }

        private void DeserializeAndSave()
        {
            Clear();
            var requests = _deserializeService.DeserializeAllFiles();
            WriteLine("All logs for found files:\n");
            _logger.PrintLogs();

            WriteLine("\nPress any key to continue.");
            ReadKey();
            requests.ToList().ForEach(r => _provider.Add(r));
        }

        private void LoadDirectory()
        {
            while (true)
            {
                try
                {
                    Clear();
                    WriteLine($"To begin, enter the directory path that contains the files to be processed.\n" +
                        $"Supported files extensions: \"{string.Join(", ", _filesReader.SupportedExtensions)}\"\n");

                    Write("Path: ");
                    var dirPath = ReadLine();
                    _filesReader.ReadFiles(dirPath, SearchOption.AllDirectories);
                    break;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                    ReadKey();
                }
            }
        }
    }
}
