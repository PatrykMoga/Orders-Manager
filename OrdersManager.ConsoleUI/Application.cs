using OrdersManager.Core;
using OrdersManager.Core.Importers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.ConsoleUI
{
    public class Application
    {
        private readonly IDeserializeService _service;
        private readonly IFilesReader _reader;

        public Application(IDeserializeService service, IFilesReader reader)
        {
            _service = service;
            _reader = reader;
        }
        public void Run()
        {
            _reader.ReadFiles(@"D:\TestFolder\Inner", System.IO.SearchOption.AllDirectories);
            _service.DeserializeAllFiles();
        }
    }
}
