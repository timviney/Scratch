namespace WebApplication1;

public class ProductService
{
    List<Product> _products = new();
    
    private int _nextId = 0;
    private int NextId()
    {
        return Interlocked.Increment(ref _nextId);
    }
    
    public IEnumerable<Product> GetProducts() => _products;
    
    public Product RegisterProduct(string name, double price)
    {
        var product = new Product(NextId(), name, price);
        _products.Add(product);
        return product;
    }

    public Product? GetProduct(int id) => GetProducts().FirstOrDefault(p => p.Id == id); //obviously slow, would hash for faster retrieval IRL

    public Product? DeleteProduct(int id)
    {
        var product = GetProduct(id);
        if (product == null) return null;
        _products.Remove(product);
        return product;
    }
}