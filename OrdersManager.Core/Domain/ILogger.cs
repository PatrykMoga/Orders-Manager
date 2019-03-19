namespace OrdersManager.Core.Domain
{
    public interface ILogger
    {
        void AddException(string message);
        void LogExcepltions();
    }
}