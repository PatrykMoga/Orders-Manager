namespace OrdersManager.Core.Data
{
    public interface IRequest
    {
        string ClientId { get; set; }
        string Name { get; set; }
        decimal? Price { get; set; }
        int? Quantity { get; set; }
        long? RequestId { get; set; }
    }
}