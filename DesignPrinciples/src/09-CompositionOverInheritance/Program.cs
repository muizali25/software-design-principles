using DesignPrinciples.CompositionOverInheritance.Good;

var report = new ReportService(
    new SalesReportDataSource(125_000m, includeChart: true),
    new TextReportFormatter(),
    new PdfReportExporter());

Console.WriteLine(report.Create());
