using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Repository;
using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace OrdersManager.ConsoleUI
{
    public class LoadFiles
    {
        private readonly IFilesReader _filesReader;
        private readonly IDeserializeService _deserializeService;
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        private readonly IMenuService _menuService;
        
        public LoadFiles(IFilesReader filesReader, IDeserializeService deserializeService,
            IRepository repository, ILogger logger, IMenuService menuService)
        {
            _filesReader = filesReader;
            _deserializeService = deserializeService;
            _repository = repository;
            _logger = logger;
            _menuService = menuService;
        }
        
        public void Start()
        {
            LoadDirectory();

            Clear();
            var requests = _deserializeService.DeserializeAllFiles();
            _logger.PrintLogs();
            ReadLine();
            WriteLine("Czy załadować pliki do pamięci i kontynuować?");
            requests.ToList().ForEach(r => _repository.Insert(r));

            while (true)
            {
                _menuService.PrintMenu();
            }
            
           
        }

        private void LoadDirectory()
        {
            while (true)
            {
                try
                {
                    Clear();
                    WriteLine("Podaj pełną ścieżkę do folderu z plikami");
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
