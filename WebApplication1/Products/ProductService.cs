using WebApplication1.ProductRepository;

namespace WebApplication1;

public class ProductService(IProductRepository repository)
{
    public IEnumerable<Product> GetProducts() => repository.GetProducts();
    public Product RegisterProduct(string name, double price) => repository.RegisterProduct(name, price);
    public Product? GetProduct(int id) => repository.GetProduct(id);
    public Product? DeleteProduct(int id) => repository.DeleteProduct(id);
}