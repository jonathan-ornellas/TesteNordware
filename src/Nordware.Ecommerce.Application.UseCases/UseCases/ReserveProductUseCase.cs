using Microsoft.Extensions.Options;
using Nordware.Ecommerce.Api.Configuration;
using Nordware.Ecommerce.Application.DTOs;
using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Domain.Entities;
using Nordware.Ecommerce.Domain.Events;
using Nordware.Ecommerce.Domain.Repositories;
using Nordware.Ecommerce.Infrastructure.Messaging;


namespace Nordware.Ecommerce.Application.UseCases
{
    public class ReserveProductUseCase:IReserveProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ReservationSettings _reservationSettings;

        public ReserveProductUseCase(
            IProductRepository productRepository,
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            IEventPublisher eventPublisher,
            IOptions<ReservationSettings> reservationSettings)
        {
            _productRepository = productRepository;
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _eventPublisher = eventPublisher;
            _reservationSettings = reservationSettings.Value;
        }
        public async Task<ReserveProductResultDTO> ExecuteAsync(Guid productId, Guid customerId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Produto não encontrado.");

            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new Exception("Cliente não encontrado.");

            product.Reserve();

            var reservation = new Reservation(customerId, productId, _reservationSettings.ReservationExpirationInSeconds);

            await _reservationRepository.AddAsync(reservation);
            await _productRepository.UpdateAsync(product);

            var reservationEvent = new ReservationCreatedEvent(
                reservation.Id,
                customerId,
                productId,
                reservation.ExpirationDate);

            _eventPublisher.Publish(reservationEvent);

            return new ReserveProductResultDTO
            {
                ReservationId = reservation.Id,
                ProductId = product.Id,
                ProductName = product.Name
            };
        }
    }

}

