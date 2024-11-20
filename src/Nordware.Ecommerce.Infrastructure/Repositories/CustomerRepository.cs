using Microsoft.EntityFrameworkCore;
using Nordware.Ecommerce.Domain.Entities;
using Nordware.Ecommerce.Domain.Repositories;
using Nordware.Ecommerce.Infrastructure.Context;

namespace Nordware.Ecommerce.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.AsNoTracking().FirstAsync(p => p.Id == id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
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
