using OrdersManager.Core.Requests;
using CsvHelper.Configuration;
using CsvHelper;

namespace OrdersManager.Core.Importers
{
    sealed class RequestCsvMap : ClassMap<Request>
    {
        public RequestCsvMap()
        {
            Map(m => m.ClientId).Name("Client_Id");
            Map(m => m.RequestId).Name("Request_id");
            Map(m => m.Name).Name("Name");
            Map(m => m.Quantity).Name("Quantity");
            Map(m => m.Price).Name("Price");
        }
    }
}