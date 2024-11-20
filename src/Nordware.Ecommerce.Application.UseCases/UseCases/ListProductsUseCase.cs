using Nordware.Ecommerce.Application.DTOs;
using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Domain.Repositories;

namespace Nordware.Ecommerce.Application.UseCases
{
    public class ListProductsUseCase: IListProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public ListProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDTO(p.Id, p.Name,p.Price,p.Status));
        }
    }
}
