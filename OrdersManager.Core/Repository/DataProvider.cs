using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersManager.Core.Repository
{
    public class DataProvider
    {
        private readonly MemoryRepository _repository;

        public DataProvider(MemoryRepository repository)
        {
            _repository = repository;
        }
    }
}
