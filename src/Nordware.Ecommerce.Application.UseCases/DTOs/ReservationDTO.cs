using static Nordware.Ecommerce.Domain.Entities.Reservation;

namespace Nordware.Ecommerce.Application.DTOs
{
    public record ReservationDTO(
       Guid Id,
       Guid CustomerId,
       string CustomerName,
       Guid ProductId,
       string ProductName,
       DateTime ReservationDate,
       DateTime ReservationExpirationDate,
       ReservationStatus Status
   );
}
