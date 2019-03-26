﻿using OrdersManager.Core;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Logs;
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
        private readonly IRequestProvider _provider;
        private readonly ILogger _logger;

        public DataManager(IFilesReader filesReader, IDeserializeService deserializeService,
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
            Deserialize();

        }

        private void Deserialize()
        {
            Clear();
            var requests = _deserializeService.DeserializeAllFiles();
            _logger.PrintLogs();

            WriteLine("Do you want to load files into memory and start processing?");
            ReadLine();
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
                        $"Supported file extensions: \"{string.Join(", ", _filesReader.SupportedTypes)}\"\n");

                    Write("Path: ");
                    var dirPath = ReadLine();                   
                    _filesReader.ReadFiles(@"d:\testfolder\inner", SearchOption.AllDirectories);
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
