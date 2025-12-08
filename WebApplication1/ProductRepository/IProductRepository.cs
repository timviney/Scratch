namespace WebApplication1.ProductRepository;

// Repository pattern example
public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    Product RegisterProduct(string name, double price);
    Product? GetProduct(int id);
    Product? DeleteProduct(int id);
}