namespace OrdersManager.Core.Domain
{
    public interface ILogger
    {
        void LogSuccess(string message);
        void LogError(string message);
        void PrintLogs();
    }
}