using Nordware.Ecommerce.Application.DTOs;

namespace Nordware.Ecommerce.Application.Interfaces
{
    public interface IListCustomerReservationsUseCase
    {
        Task<IEnumerable<ReservationDTO>> ExecuteAsync(Guid customerId);
    }
}
