using OrdersManager.Core.Repository;

namespace OrdersManager.Core.Data
{
    public class RequestService
    {
        private readonly IRepository _repository;

        public RequestService(IRepository repository)
        {
            _repository = repository;
        }

        private bool Validate(IRequest request)
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

        public void Add(IRequest request)
        {
            if (Validate(request))
            {
                _repository.Insert(request);
            }
        }
    }
}
