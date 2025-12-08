// using WebApplication1.Products;
//
// namespace WebApplication1.ProductRepository;
//
// public class InMemoryProductRepository : IProductRepository
// {
//     private readonly List<Product> _products = [];
//     
//     private int _nextId = 0;
//     private int NextId()
//     {
//         return Interlocked.Increment(ref _nextId);
//     }
//     
//     public IEnumerable<Product> GetProducts()
//     {
//         return _products;
//     }
//
//     public Product RegisterProduct(string name, double price)
//     {
//         var product = new Product(NextId(), name, price);
//         _products.Add(product);
//         return product;
//     }
//
//     public Product? GetProduct(int id)
//     {
//         return _products.FirstOrDefault(p => p.Id == id); //obvs slow, would hash IRL
//     }
//
//     public Product? DeleteProduct(int id)
//     {
//         var product = GetProduct(id);
//         if (product == null) return null;
//         _products.Remove(product);
//         return product;
//     }
// }