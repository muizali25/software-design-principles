using Bad = DesignPrinciples.SRP.Bad;
using Good = DesignPrinciples.SRP.Good;

var badOrder = new Bad.Order("B-100", "sam@example.com", [new("BOOK", 20m, 2)]);
Console.WriteLine($"Bad total: {new Bad.OrderService().Process(badOrder):C}");

var goodOrder = new Good.Order("G-100", "sam@example.com", [new("BOOK", 20m, 2)]);
var processor = new Good.OrderProcessor(
    new Good.OrderTotalCalculator(),
    new Good.ConsoleOrderRepository(),
    new Good.ConsoleReceiptSender(),
    new Good.SimpleInvoiceGenerator());
Console.WriteLine(processor.Process(goodOrder));
