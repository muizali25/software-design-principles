using DesignPrinciples.ConventionOverConfiguration.Good;

var registry = new ReportFormatterRegistry([new SalesReportFormatter(), new InventoryReportFormatter()]);
Console.WriteLine(registry.GetFor("Sales").Format("Q4 revenue"));
