namespace DesignPrinciples.CompositionOverInheritance.Bad;

public abstract class ReportBase
{
    public virtual string Build(string title, decimal revenue) => $"{title}: {revenue:C}";
}

public class PdfReport : ReportBase
{
    public override string Build(string title, decimal revenue) => $"PDF::{base.Build(title, revenue)}";
}

public class PdfSalesReport : PdfReport
{
    public override string Build(string title, decimal revenue) =>
        $"{base.Build($"Sales - {title}", revenue)}; region=all";
}

public sealed class PdfSalesReportWithCharts : PdfSalesReport
{
    // PROBLEM: Format, sales data, and chart options are locked into one inheritance chain.
    public override string Build(string title, decimal revenue) =>
        $"{base.Build(title, revenue)}; chart=bar";
}
