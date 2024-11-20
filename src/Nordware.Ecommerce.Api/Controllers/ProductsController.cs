using Microsoft.AspNetCore.Mvc;
using Nordware.Ecommerce.Api.Validators;
using Nordware.Ecommerce.Application.Interfaces;

namespace Nordware.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IListProductsUseCase _listProductsUseCase;
        private readonly IReserveProductUseCase _reserveProductUseCase;
        private readonly IGetRandomCustomerUseCase _getRandomCustomerUseCase;

        public ProductsController(
            IListProductsUseCase listProductsUseCase,
            IReserveProductUseCase reserveProductUseCase,
            IGetRandomCustomerUseCase getRandomCustomerUseCase)
        {
            _listProductsUseCase = listProductsUseCase;
            _reserveProductUseCase = reserveProductUseCase;
            _getRandomCustomerUseCase = getRandomCustomerUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _listProductsUseCase.ExecuteAsync();
            return Ok(products);
        }


        [HttpPost("{id}/reserve")]
        [ValidateGuid("id")]
        public async Task<IActionResult> Reserve(Guid id)
        {
           
            try
            {
                // Obter um cliente aleatório
                var customer = await _getRandomCustomerUseCase.ExecuteAsync();
                var reservationResult = await _reserveProductUseCase.ExecuteAsync(id, customer.Id);

                return Ok(new
                {
                    Message = "Produto reservado com sucesso.",
                    ProductId = id,
                    ProductName = reservationResult.ProductName,
                    CustomerId = customer.Id,
                    CustomerName = customer.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
