using WebApplication1.Orders;
using WebApplication1.Products;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Product>, InMemoryRepository<Product>>();
builder.Services.AddSingleton<IRepository<Order>, InMemoryRepository<Order>>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddHostedService<OrderService>(sp => sp.GetRequiredService<OrderService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Product

app.MapGet("/products", (ProductService ps) => Results.Ok(ps.GetProducts()))
    .WithName("GetAllProducts")
    .WithOpenApi();

app.MapGet("/products/{id}", (int id, ProductService ps) =>
    {
        var product = ps.GetProduct(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    })
    .WithName("GetProduct")
    .WithOpenApi();

app.MapPost("/products", (ProductDto product, ProductService ps) => Results.Ok(ps.RegisterProduct(product)))
    .WithName("RegisterProduct")
    .WithOpenApi();

app.MapDelete("/products/{id}", (int id, ProductService ps) =>
    {
        var product = ps.DeleteProduct(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    })
    .WithName("DeleteProduct")
    .WithOpenApi();

#endregion

#region Orders

app.MapPost("/orders",
    (int productId, int quantity, bool isExpress, OrderService os, IRepository<Product> productRepository) =>
    {
        var product = productRepository.Get(productId);
        if (product is null) return Results.NotFound();

        var order = new OrderDto(product, quantity, isExpress ? new ExpressShipping() : new BasicShipping());
        var placedOrder = os.PlaceOrder(order);
        return Results.Ok(placedOrder);
    });

app.MapGet("/orders", (OrderService os) => Results.Ok(os.FilledOrders()));

#endregion

app.Run();