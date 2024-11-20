using Microsoft.Extensions.Options;
using Nordware.Ecommerce.Api.Configuration;
using Nordware.Ecommerce.Application.Interfaces;

namespace Nordware.Ecommerce.Api
{
    public class ReservationExpirationHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval;
        public ReservationExpirationHostedService(IServiceProvider serviceProvider, IOptions<ReservationSettings> reservationSettings)
        {

            _serviceProvider = serviceProvider;
            _checkInterval = TimeSpan.FromSeconds(reservationSettings.Value.ReservationExpirationCheckIntervalInSeconds);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var expirationService = scope.ServiceProvider.GetRequiredService<IReservationExpirationService>();

                        await expirationService.ExpireReservationsAsync();
                    }

                    await Task.Delay(_checkInterval, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no ReservationExpirationHostedService: {ex.Message}");
            }
        }
    }
}
