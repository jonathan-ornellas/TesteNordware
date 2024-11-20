using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Domain.Entities;
using Nordware.Ecommerce.Domain.Repositories;

namespace Nordware.Ecommerce.Application.UseCases
{
    public class GetRandomCustomerUseCase : IGetRandomCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetRandomCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> ExecuteAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            if (customers == null || customers.Count == 0)
                throw new Exception("Nenhum cliente cadastrado.");

            var random = new Random();
            int index = random.Next(customers.Count);
            return customers[index];
        }
    }
}
