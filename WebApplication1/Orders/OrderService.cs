using System.Collections.Concurrent;
using WebApplication1.Repositories;

namespace WebApplication1.Orders;

// Example Background Service
public class OrderService(IRepository<Order> orderRepository) : BackgroundService
{
    private readonly ConcurrentQueue<Order> _orders = new();
    private readonly List<OrderResult> _filledOrders = new();
    
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

    private void FillOrder(Order orderToBeFilled)
    {
        var filled = orderToBeFilled.ShippingMethod.FillOrder(orderToBeFilled);
        _filledOrders.Add(filled);
    }
    
    public List<OrderResult> FilledOrders() => _filledOrders;

    public Order PlaceOrder(OrderDto order)
    {
        var placedOrder = orderRepository.Add(order);
        _orders.Enqueue(placedOrder);
        return placedOrder;
    }
}