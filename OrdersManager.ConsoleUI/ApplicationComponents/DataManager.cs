using OrdersManager.Core;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Logs;
using OrdersManager.Core.Repository;
using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public class DataManager : IDataManager
    {
        private readonly IFilesReader _filesReader;
        private readonly IDeserializeService _deserializeService;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public DataManager(IFilesReader filesReader, IDeserializeService deserializeService, 
            IRepository repository, ILogger logger)
        {
            _filesReader = filesReader;
            _deserializeService = deserializeService;
            _repository = repository;
            _logger = logger;
        }

        public void Initialize()
        {
            LoadDirectory();
            Deserialize();

        }

        private void Deserialize()
        {
            Clear();
            var requests = _deserializeService.DeserializeAllFiles();
            _logger.PrintLogs();

            WriteLine("Czy załadować pliki do pamięci i kontynuować?");
            ReadLine();
            requests.ToList().ForEach(r => _repository.Insert(r));
        }

        private void LoadDirectory()
        {
            while (true)
            {
                try
                {
                    Clear();
                    WriteLine($"Aby rozpocząć podaj pełną ścieżkę do folderu z plikami " +
                        $"(Obsługiwane rozszerzenia: {string.Join(", ", _filesReader.SupportedTypes)})");
                    Write("Ścieżka: ");
                    var dirPath = ReadLine();
                    _filesReader.ReadFiles(@"D:\TestFolder\Inner", SearchOption.AllDirectories);
                    break;
                }
                catch (UnauthorizedAccessException ex)
                {
                    WriteLine("Błąd dostępu");
                    WriteLine(ex.Message);
                    ReadKey();
                }
                catch (DirectoryNotFoundException ex)
                {
                    WriteLine("Podana złą ścieżkę do folderu");
                    WriteLine(ex.Message);
                    ReadKey();
                }
                catch (Exception ex)
                {
                    WriteLine("Błąd");
                    WriteLine(ex.Message);
                    ReadKey();
                }
            }
        }
    }
}
