using OrdersManager.Core.Requests;

namespace OrdersManager.Core.Deserializers
{
    public static class RequestValidator
    {
        public static bool ValidateRequest(IRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId) || request.ClientId.Length > 6 || request.ClientId.Contains(" "))
                return false;

            if (request.RequestId == null)
                return false;

            if (request.Name == null || request.Name.Length > 255)
                return false;

            if (request.Price == null)
                return false;
            if (request.Quantity == null)
                return false;

            return true;
        }
    }
}