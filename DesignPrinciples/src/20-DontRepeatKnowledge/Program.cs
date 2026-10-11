using DesignPrinciples.DontRepeatKnowledge.Good;

var order = new Order(OrderStatus.Paid);
order.Cancel();
Console.WriteLine($"Order status: {order.Status}");
