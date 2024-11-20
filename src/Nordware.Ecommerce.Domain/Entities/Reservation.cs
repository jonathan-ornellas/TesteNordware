using System.ComponentModel.DataAnnotations;

namespace Nordware.Ecommerce.Domain.Entities
{
    public class Reservation : EntityBase
    {
        public enum ReservationStatus
        {
            [Display(Name = "Ativa")]
            Active,
            [Display(Name = "Expirada")]
            Expired
        }

        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public DateTime ReservationDate { get; private set; }
        public DateTime ExpirationDate { get; private set; } 
        public ReservationStatus Status { get; private set; }

        public virtual Customer Customer { get; private set; }
        public virtual Product Product { get; private set; }

        public Reservation(Guid customerId, Guid productId, int expirationInSeconds)
        {
            CustomerId = customerId;
            ProductId = productId;
            ReservationDate = DateTime.UtcNow;
            ExpirationDate = ReservationDate.AddSeconds(expirationInSeconds);
            Status = ReservationStatus.Active;
        }

        protected Reservation() { }
        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpirationDate;
        }

        public void MarkAsExpired()
        {
            Status = ReservationStatus.Expired;
        }
    }
}
