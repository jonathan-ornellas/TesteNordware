namespace Nordware.Ecommerce.Application.Interfaces
{
    public interface IReservationExpirationService
    {
        Task ExpireReservationsAsync();
    }
}
