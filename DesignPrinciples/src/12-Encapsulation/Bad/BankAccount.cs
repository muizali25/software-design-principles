namespace DesignPrinciples.Encapsulation.Bad;

public sealed class BankAccount
{
    // PROBLEM: Any caller can create an impossible account state.
    public decimal Balance;
    public List<string> Transactions = [];
}
