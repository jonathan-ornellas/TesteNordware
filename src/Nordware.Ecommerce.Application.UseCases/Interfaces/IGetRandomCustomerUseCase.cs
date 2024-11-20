using Nordware.Ecommerce.Domain.Entities;

namespace Nordware.Ecommerce.Application.Interfaces
{
    public interface IGetRandomCustomerUseCase
    {
        Task<Customer> ExecuteAsync();
    }
}
