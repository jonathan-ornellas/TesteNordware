using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Domain.Repositories;

namespace Nordware.Ecommerce.Application.Services
{
    public class ReservationExpirationService : IReservationExpirationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationExpirationService(
            IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task ExpireReservationsAsync()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllActiveReservationsAsync();

                foreach (var reservation in reservations)
                {
                    if (reservation.IsExpired())
                    {
                        reservation.MarkAsExpired();

                        reservation.Product.MakeAvailable();

                        await _reservationRepository.UpdateAsync(reservation);

                        Console.WriteLine($"Reserva expirada: {reservation.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao expirar reservas: {ex.Message}");
            }
        }
    }
}
