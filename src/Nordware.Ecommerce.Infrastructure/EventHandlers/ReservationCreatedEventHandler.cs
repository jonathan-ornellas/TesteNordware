using Nordware.Ecommerce.Domain.Events;
using Nordware.Ecommerce.Domain.Repositories;
using Nordware.Ecommerce.Infrastructure.Messaging;

namespace Nordware.Ecommerce.Infrastructure.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IProductRepository _productRepository;

        public ReservationCreatedEventHandler(
            IReservationRepository reservationRepository,
            IProductRepository productRepository)
        {
            _reservationRepository = reservationRepository;
            _productRepository = productRepository;
        }


        public void Handle(ReservationCreatedEvent @event)
        {
            Console.WriteLine($"Reserva criada: {@event.ReservationId}");
        }
    }
}
