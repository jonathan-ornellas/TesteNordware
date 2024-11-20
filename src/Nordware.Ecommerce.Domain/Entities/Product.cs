using System.ComponentModel.DataAnnotations;

namespace Nordware.Ecommerce.Domain.Entities
{
    public enum ProductStatus
    {

        [Display(Name = "Disponível")]
        Available,

        [Display(Name = "Reservado")]
        Reserved,

        [Display(Name = "Indisponível")]
        Unavailable
    }

    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductStatus Status { get; private set; }

        public Product(string name, decimal price, ProductStatus status)
        {
            Name = name;
            Price = price;
            Status = status;
        }

        protected Product() { }

        public void SetAvailable()
        {
            Status = ProductStatus.Available;
        }

        public void Reserve()
        {
            if (Status != ProductStatus.Available)
                throw new InvalidOperationException("O produto não está disponível para reserva.");

            Status = ProductStatus.Reserved;
        }

        public void MakeAvailable()
        {
            Status = ProductStatus.Available;
        }
    }
}
