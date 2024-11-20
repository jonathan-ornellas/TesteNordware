namespace Nordware.Ecommerce.Domain.Events
{
    public class ReservationCreatedEvent
    {
        public Guid ReservationId { get; }
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public DateTime ExpirationDate { get; }

        public ReservationCreatedEvent(Guid reservationId, Guid customerId, Guid productId, DateTime expirationDate)
        {
            ReservationId = reservationId;
            CustomerId = customerId;
            ProductId = productId;
            ExpirationDate = expirationDate;
        }
    }
}
