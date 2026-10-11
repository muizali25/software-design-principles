using DesignPrinciples.DRY.Good;

var calculator = new PricingCalculator();
var lines = new[]
{
    new PriceLine("LAPTOP-15", 1_200m, 1),
    new PriceLine("DOCK-USB-C", 180m, 2)
};

var checkout = new CheckoutService(calculator);
var invoice = new InvoiceService(calculator);

Console.WriteLine($"Checkout total: {checkout.CalculateTotal(lines, 10m):C}");
Console.WriteLine($"Invoice total:  {invoice.CalculateTotal(lines, 10m):C}");
