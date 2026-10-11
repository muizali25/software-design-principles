using DesignPrinciples.KISS.Good;

var calculator = new DiscountCalculator();
var discount = calculator.CalculatePercentageDiscount(250m, 15m);

Console.WriteLine($"Discount: {discount:C}");
Console.WriteLine($"Order total: {250m - discount:C}");
