using System;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.Core.Domain
{
    public class ConsoleLogger : ILogger
    {
        private List<string> _logs = new List<string>();
        public bool IsLogged { get; set; }

        public void Log(string message)
        {
            _logs.Add(message);
        }

        public void PrintLogs() => _logs.ForEach(m => WriteLine(m));
    }
}
