namespace OrdersManager.Core.Data
{
    public class Request : IRequest
    {
        public string ClientId { get; set; }
        public long? RequestId { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
