using DesignPrinciples.FailFast.Good;

var order = new OrderIntakeService().Accept("Ada Lovelace", 2);
Console.WriteLine($"Accepted {order.Quantity} items for {order.CustomerName}.");
