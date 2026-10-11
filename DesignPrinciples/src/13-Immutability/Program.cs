using DesignPrinciples.Immutability.Good;

var line = new OrderLine("BOOK-1", new Money(25m, "USD"), 2);
var revisedLine = line.WithQuantity(3);
Console.WriteLine($"Original: {line.Total.Amount}; revised: {revisedLine.Total.Amount}");
