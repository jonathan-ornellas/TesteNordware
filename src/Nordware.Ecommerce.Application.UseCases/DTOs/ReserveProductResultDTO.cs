namespace Nordware.Ecommerce.Application.DTOs
{
    public class ReserveProductResultDTO
    {
        public Guid ReservationId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
