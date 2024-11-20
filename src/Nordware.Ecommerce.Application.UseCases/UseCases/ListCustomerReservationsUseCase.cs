using Nordware.Ecommerce.Application.DTOs;
using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Domain.Repositories;

namespace Nordware.Ecommerce.Application.UseCases
{
    public class ListCustomerReservationsUseCase: IListCustomerReservationsUseCase
    {
        private readonly IReservationRepository _reservationRepository;

        public ListCustomerReservationsUseCase(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationDTO>> ExecuteAsync(Guid customerId)
        {
            var reservations = await _reservationRepository.GetByCustomerIdWithDetailsAsync(customerId);
            return reservations.Select(r => new ReservationDTO(
              r.Id,
              r.CustomerId,
              r.Customer.Name,
              r.ProductId,
              r.Product.Name,
              r.ReservationDate,
              r.ExpirationDate,
              r.Status
      ));
        }
    }
}
