namespace WebApplication1.Orders.ExternalOrderRecordingService;

public record ExternalSystemOrderRecord(string Name, double Price, int Quantity, string ShippingMethod, string ShippingTime);