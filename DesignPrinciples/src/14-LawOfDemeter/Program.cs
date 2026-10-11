using DesignPrinciples.LawOfDemeter.Good;

var order = new Order("ORD-42", new ShippingAddress("1 Main St", "Seattle", "98101"));
Console.WriteLine(new ShippingLabelPrinter().Print(order));
