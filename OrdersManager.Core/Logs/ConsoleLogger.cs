using System;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.Core.Logs
{
    public class ConsoleLogger : ILogger
    {
        private readonly IList<(string logType, string message)> _logs;

        public ConsoleLogger()
        {
            _logs = new List<(string, string)>();
        }

        public void LogError(string message) => _logs.Add(("error", message));

        public void LogSuccess(string message) => _logs.Add(("success", message));

        public void PrintLogs()
        {
            foreach (var (logType, message) in _logs)
            {
                if (logType == "error")
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(message);
                }

                if (logType == "success")
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(message);
                }
            }
            ForegroundColor = ConsoleColor.White;
        }
    }
}
