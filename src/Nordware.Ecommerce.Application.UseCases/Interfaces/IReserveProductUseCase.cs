using Nordware.Ecommerce.Application.DTOs;

namespace Nordware.Ecommerce.Application.Interfaces
{
    public interface IReserveProductUseCase
    {
        Task<ReserveProductResultDTO> ExecuteAsync(Guid productId, Guid customerId);
    }
}
