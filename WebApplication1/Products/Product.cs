using WebApplication1.Repositories;

namespace WebApplication1.Products;

public record Product(int Id, string Name, double Price) : IEntity;

public record ProductDto(string Name, double Price) : IEntityDto<Product>
{
    public Product Create(int id) => new Product(id, Name, Price);
}