using DesignPrinciples.Encapsulation.Good;

namespace DesignPrinciples.Encapsulation.Tests;

public sealed class BankAccountTests
{
    [Fact]
    public void Deposit_ValidAmount_UpdatesBalanceAndLedger()
    {
        var account = new BankAccount("ACC-1");
        account.Deposit(75m, "Gift");
        Assert.Equal(75m, account.Balance);
        Assert.Single(account.Transactions);
    }

    [Fact]
    public void Withdraw_ExceedingBalance_PreservesInvariant()
    {
        var account = new BankAccount("ACC-1", 20m);
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(21m, "Purchase"));
        Assert.Equal(20m, account.Balance);
    }
}
