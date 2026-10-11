namespace DesignPrinciples.LSP.Bad;

public sealed record RefundRequest(string OrderId, decimal PaidAmount);

public abstract class RefundProcessor
{
    // Clients reasonably rely on this contract returning the full paid amount.
    public abstract decimal Refund(RefundRequest request);
}

public sealed class CardRefundProcessor : RefundProcessor
{
    public override decimal Refund(RefundRequest request) => request.PaidAmount;
}

public sealed class StoreCreditRefundProcessor : RefundProcessor
{
    public override decimal Refund(RefundRequest request)
    {
        // PROBLEM: this subtype rejects a valid base operation and breaks callers.
        throw new NotSupportedException("Store credit is not a cash refund.");
    }
}
