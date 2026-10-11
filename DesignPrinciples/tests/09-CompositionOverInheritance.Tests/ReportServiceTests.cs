using DesignPrinciples.CompositionOverInheritance.Good;

namespace DesignPrinciples.CompositionOverInheritance.Tests;

public sealed class ReportServiceTests
{
    [Fact]
    public void Create_ComposesDataFormattingAndExport()
    {
        var service = new ReportService(
            new SalesReportDataSource(500m, includeChart: true),
            new TextReportFormatter(),
            new PdfReportExporter());

        var document = service.Create();

        Assert.StartsWith("PDF::Quarterly Sales:", document);
        Assert.Contains("[Bar chart]", document);
    }

    [Fact]
    public void Formatter_CanExcludeChartIndependently()
    {
        var content = new TextReportFormatter().Format(new ReportData("Sales", 50m, false));

        Assert.DoesNotContain("chart", content, StringComparison.OrdinalIgnoreCase);
    }
}
