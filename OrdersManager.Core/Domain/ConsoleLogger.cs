using System;
using System.Collections.Generic;

namespace OrdersManager.Core.Domain
{
    public class ConsoleLogger : ILogger
    {
        private List<Exception> _exceptions = new List<Exception>();

        public void AddException(string message) => _exceptions.Add(new Exception(message));

        public void LogExcepltions() => _exceptions.ForEach(ex => Console.Write(ex.Message));
    }
}
