using Nordware.Ecommerce.Domain.Entities;

namespace Nordware.Ecommerce.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Reservation>> GetExpiredReservationsAsync();
        Task<Reservation> GetByIdAsync(Guid id);
        Task RemoveAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetByCustomerIdWithDetailsAsync(Guid customerId);
        Task<List<Reservation>> GetAllActiveReservationsAsync(); 
    
    }
}
