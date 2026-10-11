using DesignPrinciples.TellDontAsk.Good;

var order = new Order();
order.AddItem("SKU-1");
order.MarkPaid();
new OrderShippingService().Ship(order);
Console.WriteLine($"Order status: {order.Status}");
