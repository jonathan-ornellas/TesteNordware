using Nordware.Ecommerce.Domain.Entities;

namespace Nordware.Ecommerce.Application.DTOs
{
    public record ProductDTO(Guid Id, string Name, decimal Price, ProductStatus Status);

}
