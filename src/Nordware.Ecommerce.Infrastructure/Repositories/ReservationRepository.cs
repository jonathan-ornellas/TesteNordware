using Microsoft.EntityFrameworkCore;
using Nordware.Ecommerce.Domain.Entities;
using Nordware.Ecommerce.Domain.Repositories;
using Nordware.Ecommerce.Infrastructure.Context;
using static Nordware.Ecommerce.Domain.Entities.Reservation;

namespace Nordware.Ecommerce.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Reservation>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Reservations
                .Where(r => r.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetExpiredReservationsAsync()
        {
            return await _context.Reservations
                .Where(r => r.IsExpired())
                .ToListAsync();
        }

        public async Task RemoveAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.AsNoTracking().FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetByCustomerIdWithDetailsAsync(Guid customerId)
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .Where(r => r.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetAllActiveReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Product)
                .Where(r => r.Status == ReservationStatus.Active)
                .ToListAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
               
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
