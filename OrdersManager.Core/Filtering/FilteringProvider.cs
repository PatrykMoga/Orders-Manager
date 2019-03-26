﻿using OrdersManager.Core.Data;
using OrdersManager.Core.Repository;
using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using OrdersManager.Core.Extensions;

namespace OrdersManager.Core.Filtering
{
    public class FilteringProvider : IFilteringProvider
    {
        public string SerachPattern { get; private set; }
        private readonly IRepository _repository;
        private readonly Dictionary<int, RequestFilter> _filters;

        public FilteringProvider(IRepository repository)
        {
            _repository = repository;

            _filters = new Dictionary<int, RequestFilter>();
            _filters.Add(1, new RequestFilter("All", r => true));
            _filters.Add(2, new RequestFilter("Client-Id:", r => r.ClientId == SerachPattern, ValidateClientId));
        }      

        public Dictionary<int, RequestFilter> GetFilters() => _filters;

        private IEnumerable<string> ClientIds => _repository.GetAll().Select(r => r.ClientId).Distinct();

        private void ValidateClientId()
        {
            while (true)
            {
                Clear();
                WriteLine("Available client Ids:");
                var ids = string.Join(", ", ClientIds);
                WriteLine(ids.PrintInLines('-'));
                Write("Enter Client Id: ");
                var clientId = ReadLine();
                if (_repository.Contains(r => r.ClientId == clientId))
                {
                    SerachPattern = clientId;
                    break;
                }
                else
                {
                    WriteLine("Client doesn't exist");
                }
                ReadKey();
            }
        }       
    }
}