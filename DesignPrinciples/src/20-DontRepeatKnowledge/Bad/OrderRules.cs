namespace DesignPrinciples.DontRepeatKnowledge.Bad;

public sealed class Checkout
{
    public bool CanCancel(string status) => status == "P" || status == "PAID";
}

public sealed class Refunds
{
    public bool CanCancel(string status) => status == "Paid" || status == "P";
}
