namespace DesignPrinciples.Encapsulation.Good;

public sealed class BankAccount
{
    private decimal _balance;
    private readonly List<Transaction> _transactions = [];

    public BankAccount(string accountNumber, decimal openingBalance = 0)
    {
        if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("Account number is required.", nameof(accountNumber));
        if (openingBalance < 0) throw new ArgumentOutOfRangeException(nameof(openingBalance));
        AccountNumber = accountNumber;
        _balance = openingBalance;
        if (openingBalance > 0) _transactions.Add(new Transaction("Opening balance", openingBalance));
    }

    public string AccountNumber { get; }
    public decimal Balance => _balance;
    public IReadOnlyList<Transaction> Transactions => _transactions;

    public void Deposit(decimal amount, string description)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        _balance += amount;
        _transactions.Add(new Transaction(description, amount));
    }

    public void Withdraw(decimal amount, string description)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (amount > _balance) throw new InvalidOperationException("Insufficient funds.");
        _balance -= amount;
        _transactions.Add(new Transaction(description, -amount));
    }
}

public sealed record Transaction(string Description, decimal Amount);
