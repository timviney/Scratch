namespace WebApplication1.Orders;

// Example Strategy Pattern
public interface IShipping
{
    public TimeSpan Calculate(Order order);
}

public class BasicShipping : IShipping
{
    public TimeSpan Calculate(Order order)
    {
        return TimeSpan.FromDays(Random.Shared.NextDouble() * 3);
    }
}

public class ExpressShipping : IShipping
{
    public TimeSpan Calculate(Order order)
    {
        return TimeSpan.FromDays(Random.Shared.NextDouble());
    }
}

