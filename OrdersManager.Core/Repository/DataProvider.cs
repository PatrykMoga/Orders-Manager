using OrdersManager.Core.Importers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Repository
{
    public class DataProvider
    {
        private readonly IRepository _repository;
        private readonly IDeserializeService _service;

        public DataProvider(IRepository repository, IDeserializeService service)
        {
            _repository = repository;
            _service = service;
        }
    }
}
