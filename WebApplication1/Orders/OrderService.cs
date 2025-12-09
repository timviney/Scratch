using System.Collections.Concurrent;
using WebApplication1.Orders.ExternalOrderRecordingService;
using WebApplication1.Repositories;

namespace WebApplication1.Orders;

// Example Background Service
public class OrderService(IRepository<Order> orderRepository) : BackgroundService
{
    private readonly ConcurrentQueue<Order> _orders = new();
    private readonly List<ExternalSystemOrderRecord> _filledOrders = new();
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            _orders.TryDequeue(out var order);
            if (order != null)
            {
                FillOrder(order);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    private void FillOrder(Order order)
    {
        // here we use an adapter to record our data on an "external service"
        var orderRecorder = new OrderResultsAdapter(order, order.ShippingMethod.Calculate(order));
        
        var recordedOrder = ExternalOrderRecordingService.ExternalService.FillOrder(orderRecorder);
        _filledOrders.Add(recordedOrder);
    }
    
    public List<ExternalSystemOrderRecord> FilledOrders() => _filledOrders;

    public Order PlaceOrder(OrderDto order)
    {
        var placedOrder = orderRepository.Add(order);
        _orders.Enqueue(placedOrder);
        return placedOrder;
    }
}