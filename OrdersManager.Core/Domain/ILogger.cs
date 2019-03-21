namespace OrdersManager.Core.Domain
{
    public interface ILogger
    {
        bool IsLogged { get; set; }
        void Log(string message);
        void PrintLogs();
    }
}