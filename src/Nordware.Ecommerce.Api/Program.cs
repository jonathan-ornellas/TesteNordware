using Microsoft.EntityFrameworkCore;
using Nordware.Ecommerce.Api;
using Nordware.Ecommerce.Api.Configuration;
using Nordware.Ecommerce.Api.Helps;
using Nordware.Ecommerce.Application.Interfaces;
using Nordware.Ecommerce.Application.Services;
using Nordware.Ecommerce.Application.UseCases;
using Nordware.Ecommerce.Domain.Entities;
using Nordware.Ecommerce.Domain.Events;
using Nordware.Ecommerce.Domain.Repositories;
using Nordware.Ecommerce.Infrastructure.Context;
using Nordware.Ecommerce.Infrastructure.EventBus;
using Nordware.Ecommerce.Infrastructure.EventHandlers;
using Nordware.Ecommerce.Infrastructure.Messaging;
using Nordware.Ecommerce.Infrastructure.Repositories;
using static Nordware.Ecommerce.Domain.Entities.Reservation;

var builder = WebApplication.CreateBuilder(args);

#region Configuração de Serviços

builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.Converters.Add(new DisplayNameEnumConverter<ProductStatus>());
           options.JsonSerializerOptions.Converters.Add(new DisplayNameEnumConverter<ReservationStatus>());
       });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Configuração de Dependências

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<IReserveProductUseCase, ReserveProductUseCase>();
builder.Services.AddScoped<IListProductsUseCase, ListProductsUseCase>();
builder.Services.AddScoped<IListCustomerReservationsUseCase, ListCustomerReservationsUseCase>();
builder.Services.AddScoped<IGetRandomCustomerUseCase, GetRandomCustomerUseCase>();

builder.Services.AddSingleton<IEventPublisher, InMemoryEventBus>();

builder.Services.AddScoped<IEventHandler<ReservationCreatedEvent>, ReservationCreatedEventHandler>();

builder.Services.AddScoped<IReservationExpirationService, ReservationExpirationService>();
builder.Services.AddHostedService<ReservationExpirationHostedService>();

builder.Services.Configure<ReservationSettings>(builder.Configuration.GetSection("ReservationSettings"));


#endregion

var app = builder.Build();

#region Configuração do Pipeline de Solicitações

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

#endregion

#region Data

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var products = new List<Product>
    {
        new Product("Notebook Dell XPS 13", 9999.99m, ProductStatus.Available),
        new Product("Smartphone Samsung Galaxy S21", 4999.99m, ProductStatus.Available),
        new Product("Mouse Logitech MX Master 3", 399.99m,  ProductStatus.Unavailable),
        new Product("Monitor LG UltraWide 29", 1299.99m, ProductStatus.Available),
        new Product("Teclado Mecânico Razer BlackWidow", 799.99m, ProductStatus.Available),
        new Product("Headset HyperX Cloud II", 599.99m, ProductStatus.Available),
        new Product("Placa de Vídeo NVIDIA RTX 3080", 6999.99m, ProductStatus.Unavailable),
        new Product("Processador Intel Core i9", 3299.99m, ProductStatus.Available),
        new Product("Memória RAM Corsair Vengeance 16GB", 799.99m,ProductStatus.Available),
        new Product("SSD Samsung 970 EVO Plus 1TB", 1099.99m, ProductStatus.Available)
    };

    context.Products.AddRange(products);

    var customers = new List<Customer>
    {
        new Customer(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "João Silva"),
        new Customer(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Maria Oliveira"),
        new Customer(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Pedro Santos"),
        new Customer(new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Ana Costa"),
        new Customer(new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Lucas Pereira")
    };

    context.Customers.AddRange(customers);

    context.SaveChanges();
}

#endregion

#region Processamento de Eventos

var eventBus = app.Services.GetRequiredService<IEventPublisher>() as InMemoryEventBus;
app.Lifetime.ApplicationStarted.Register(() =>
{
    Task.Run(async () =>
    {
        while (true)
        {
            eventBus?.ProcessEvents();
            await Task.Delay(1000); 
        }
    });
});

#endregion

app.Run();