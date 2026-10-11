using DesignPrinciples.LSP.Good;

namespace DesignPrinciples.LSP.Tests;

public sealed class RefundPolicyTests
{
    [Theory]
    [InlineData("original payment method")]
    [InlineData("store credit")]
    public void Each_Policy_Honors_The_Same_Refund_Contract(string expectedDestination)
    {
        IRefundPolicy policy = expectedDestination == "store credit"
            ? new StoreCreditRefundPolicy()
            : new OriginalPaymentRefundPolicy();

        var result = policy.Refund(new RefundRequest("1", 33m));

        Assert.Equal(33m, result.Amount);
        Assert.Equal(expectedDestination, result.Destination);
    }

    [Fact]
    public void Service_Uses_The_Configured_Policy()
    {
        var result = new RefundService(new StoreCreditRefundPolicy()).Process(new RefundRequest("1", 10m));

        Assert.Equal("store credit", result.Destination);
    }
}
