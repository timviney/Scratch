namespace WebApplication1.Orders.ExternalOrderRecordingService;

// Example Adapter Pattern
public interface IOrderResultCreator
{
    public ExternalSystemOrderRecord GetResult();
}

public class OrderResultsAdapter(Order order, TimeSpan elapsedTime) : IOrderResultCreator
{
    // Here we adapt the Order record from our system into an order record for the External System
    public ExternalSystemOrderRecord GetResult()
    {
        var shippingTimeStr =  "Shipping time: " + (elapsedTime.Days==0 ? "" : $"{elapsedTime.Days} days & ") + elapsedTime.TotalHours + " hours";
        
        return new ExternalSystemOrderRecord(order.Product.Name, order.Product.Price, order.Quantity, order.ShippingMethod.GetType().Name, shippingTimeStr);
    }
}

public static class ExternalService
{
    // Example of an external service that needs a creator in the format it expects, so we have to pass an adapter in here instead
    public static ExternalSystemOrderRecord FillOrder(IOrderResultCreator orderResultCreator) => orderResultCreator.GetResult();
}