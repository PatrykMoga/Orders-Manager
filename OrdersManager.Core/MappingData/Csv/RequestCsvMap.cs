using CsvHelper.Configuration;
using OrdersManager.Core.Data;

namespace OrdersManager.Core.MappingData.Csv
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