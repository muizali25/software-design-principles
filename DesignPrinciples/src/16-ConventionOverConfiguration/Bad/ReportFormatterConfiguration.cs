namespace DesignPrinciples.ConventionOverConfiguration.Bad;

public sealed class ReportFormatterConfiguration
{
    private readonly Dictionary<string, string> _formatters = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Sales"] = "DesignPrinciples.ConventionOverConfiguration.Bad.SalesReportFormatter",
        ["Inventory"] = "DesignPrinciples.ConventionOverConfiguration.Bad.InventoryReportFormatter",
        ["Customer"] = "DesignPrinciples.ConventionOverConfiguration.Bad.CustomerReportFormatter",
        ["Audit"] = "DesignPrinciples.ConventionOverConfiguration.Bad.AuditReportFormatter"
    };

    public string GetFormatterClassName(string reportName) =>
        _formatters.TryGetValue(reportName, out var className)
            ? className
            : throw new ArgumentException($"No formatter is configured for '{reportName}'.", nameof(reportName));
}
