using Nordware.Ecommerce.Domain.Entities;

namespace Nordware.Ecommerce.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task<List<Customer>> GetAllAsync();

    }
}
