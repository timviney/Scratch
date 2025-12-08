namespace WebApplication1.Orders;

// Example Strategy Pattern
public interface IShipping
{
    public OrderResult FillOrder(Order order);
}

public class BasicShipping : IShipping
{
    public OrderResult FillOrder(Order order)
    {
        return new OrderResult(order, TimeSpan.FromDays(Random.Shared.NextDouble() * 3));
    }
}

public class ExpressShipping : IShipping
{
    public OrderResult FillOrder(Order order)
    {
        return new OrderResult(order, TimeSpan.FromDays(Random.Shared.NextDouble()));
    }
}

