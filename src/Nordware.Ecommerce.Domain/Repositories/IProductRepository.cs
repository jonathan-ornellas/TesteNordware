using Nordware.Ecommerce.Domain.Entities;

namespace Nordware.Ecommerce.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product product);
    }
}
