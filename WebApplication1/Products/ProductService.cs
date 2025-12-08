using WebApplication1.Repositories;

namespace WebApplication1.Products;

public class ProductService(IRepository<Product> repository)
{
    public IEnumerable<Product> GetProducts() => repository.GetAll();
    public Product RegisterProduct(ProductDto product) => repository.Add(product);
    public Product? GetProduct(int id) => repository.Get(id);
    public Product? DeleteProduct(int id) => repository.Delete(id);
}