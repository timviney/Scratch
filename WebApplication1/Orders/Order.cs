using WebApplication1.Products;
using WebApplication1.Repositories;

namespace WebApplication1.Orders;

public record Order(int Id, Product Product, int Quantity, IShipping ShippingMethod) : IEntity;

public record OrderDto(Product Product, int Quantity, IShipping ShippingMethod) : IEntityDto<Order>
{
    public Order Create(int id) => new(id, Product, Quantity, ShippingMethod);
}