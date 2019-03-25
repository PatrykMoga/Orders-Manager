using System;
using System.Collections.Generic;
using static System.Console;

namespace OrdersManager.Core.Domain
{
    public class ConsoleLogger : ILogger
    {
        private List<(string errorType,string message)> _logs = new List<(string, string)>();

        //public void Log(string message)
        //{
        //    _logs.Add("",message);
        //}

        public void LogError(string message)
        {
            _logs.Add(("error",message));
        }

        public void LogSuccess(string message)
        {
            _logs.Add(("success",message));
        }

        public void PrintLogs()
        {
            foreach (var log in _logs)
            {
                if (log.errorType == "error")
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(log.message);                   
                }

                if (log.errorType == "success")
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(log.message);
                }
            }
            ForegroundColor = ConsoleColor.White;
        }
    }
}
