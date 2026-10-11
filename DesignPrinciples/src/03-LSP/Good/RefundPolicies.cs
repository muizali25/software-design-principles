namespace DesignPrinciples.LSP.Good;

public sealed record RefundRequest(string OrderId, decimal PaidAmount);
public sealed record RefundResult(decimal Amount, string Destination);

public interface IRefundPolicy
{
    RefundResult Refund(RefundRequest request);
}

public sealed class OriginalPaymentRefundPolicy : IRefundPolicy
{
    public RefundResult Refund(RefundRequest request) =>
        new(request.PaidAmount, "original payment method");
}

public sealed class StoreCreditRefundPolicy : IRefundPolicy
{
    public RefundResult Refund(RefundRequest request) =>
        new(request.PaidAmount, "store credit");
}

public sealed class RefundService(IRefundPolicy policy)
{
    public RefundResult Process(RefundRequest request) => policy.Refund(request);
}
