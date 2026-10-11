using DesignPrinciples.ProgramToInterface.Good;

IProductCatalog catalog = new InMemoryProductCatalog(
[
    new Product("LAPTOP-15", "15-inch Laptop", 1_200m),
    new Product("MOUSE-WL", "Wireless Mouse", 35m)
]);

var orderService = new OrderService(catalog);
Console.WriteLine($"Order total: {orderService.GetOrderTotal("LAPTOP-15", 2):C}");
