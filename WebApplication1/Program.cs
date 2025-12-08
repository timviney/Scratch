using WebApplication1;
using WebApplication1.ProductRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", (ProductService ps) => Results.Ok(ps.GetProducts()))
    .WithName("GetAllProducts")
    .WithOpenApi();

app.MapGet("/products/{id}", (int id, ProductService ps) =>
    {
        Product? product = ps.GetProduct(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    })
    .WithName("GetProduct")
    .WithOpenApi();

app.MapPost("/products", (string name, double price, ProductService ps) => Results.Ok(ps.RegisterProduct(name, price)))
    .WithName("RegisterProduct")
    .WithOpenApi();

app.MapDelete("/products/{id}", (int id, ProductService ps) =>
    {
        var product = ps.DeleteProduct(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    })
    .WithName("DeleteProduct")
    .WithOpenApi();

app.Run();