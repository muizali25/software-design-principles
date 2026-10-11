namespace DesignPrinciples.ConventionOverConfiguration.Good;

public interface IReportFormatter
{
    string Format(string content);
}

public sealed class SalesReportFormatter : IReportFormatter
{
    public string Format(string content) => $"Sales: {content}";
}

public sealed class InventoryReportFormatter : IReportFormatter
{
    public string Format(string content) => $"Inventory: {content}";
}

public sealed class ReportFormatterRegistry
{
    private readonly IReadOnlyDictionary<string, IReportFormatter> _formatters;

    public ReportFormatterRegistry(IEnumerable<IReportFormatter> formatters)
    {
        _formatters = formatters.ToDictionary(
            formatter => formatter.GetType().Name.Replace("ReportFormatter", "", StringComparison.Ordinal),
            StringComparer.OrdinalIgnoreCase);
    }

    public IReportFormatter GetFor(string reportName) =>
        _formatters.TryGetValue(reportName, out var formatter)
            ? formatter
            : throw new ArgumentException($"No '{reportName}ReportFormatter' is registered.", nameof(reportName));
}
