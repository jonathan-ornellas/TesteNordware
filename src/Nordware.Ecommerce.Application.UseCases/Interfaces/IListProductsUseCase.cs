using Nordware.Ecommerce.Application.DTOs;

namespace Nordware.Ecommerce.Application.Interfaces
{
    public interface IListProductsUseCase
    {
        Task<IEnumerable<ProductDTO>> ExecuteAsync();
    }
}
