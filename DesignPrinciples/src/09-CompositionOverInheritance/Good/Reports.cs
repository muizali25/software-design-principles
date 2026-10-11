namespace DesignPrinciples.CompositionOverInheritance.Good;

public sealed record ReportData(string Title, decimal Revenue, bool IncludeChart);

public interface IReportDataSource
{
    ReportData GetReport();
}

public interface IReportFormatter
{
    string Format(ReportData data);
}

public interface IReportExporter
{
    string Export(string content);
}

public sealed class SalesReportDataSource(decimal revenue, bool includeChart) : IReportDataSource
{
    public ReportData GetReport() => new("Quarterly Sales", revenue, includeChart);
}

public sealed class TextReportFormatter : IReportFormatter
{
    public string Format(ReportData data) =>
        $"{data.Title}: {data.Revenue:C}" + (data.IncludeChart ? Environment.NewLine + "[Bar chart]" : string.Empty);
}

public sealed class PdfReportExporter : IReportExporter
{
    public string Export(string content) => $"PDF::{content}";
}

public sealed class ReportService(
    IReportDataSource dataSource,
    IReportFormatter formatter,
    IReportExporter exporter)
{
    public string Create() => exporter.Export(formatter.Format(dataSource.GetReport()));
}
