using DesignPrinciples.ConventionOverConfiguration.Good;

namespace DesignPrinciples.ConventionOverConfiguration.Tests;

public sealed class ReportFormatterRegistryTests
{
    [Fact]
    public void GetFor_UsesFormatterClassNamingConvention()
    {
        var registry = new ReportFormatterRegistry([new SalesReportFormatter()]);

        var result = registry.GetFor("Sales").Format("Q4");

        Assert.Equal("Sales: Q4", result);
    }

    [Fact]
    public void GetFor_UnknownReport_Throws()
    {
        var registry = new ReportFormatterRegistry([]);

        Assert.Throws<ArgumentException>(() => registry.GetFor("Audit"));
    }
}
