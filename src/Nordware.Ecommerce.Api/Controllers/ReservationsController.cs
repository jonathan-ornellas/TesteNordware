using Microsoft.AspNetCore.Mvc;
using Nordware.Ecommerce.Api.Validators;
using Nordware.Ecommerce.Application.Interfaces;

namespace Nordware.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("customer/{customerId}/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IListCustomerReservationsUseCase _listReservationsUseCase;

        public ReservationsController(IListCustomerReservationsUseCase listReservationsUseCase)
        {
            _listReservationsUseCase = listReservationsUseCase;
        }

        [HttpGet]
        [ValidateGuid("customerId")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var reservations = await _listReservationsUseCase.ExecuteAsync(customerId);
            return Ok(reservations);
        }
    }
}
