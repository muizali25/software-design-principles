using DesignPrinciples.Encapsulation.Good;

var account = new BankAccount("ACC-100", 100m);
account.Deposit(50m, "Paycheck");
account.Withdraw(25m, "Groceries");
Console.WriteLine($"Balance: {account.Balance:C}; transactions: {account.Transactions.Count}");
