using System;
 public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

interface ITransactionProcessor
{
    void Process(Transaction transaction);

}
public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing bank transfer of amount {transaction.Amount} in the category {transaction.Category}");
    }
}
public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing mobile money transfer of amount {transaction.Amount} in the category {transaction.Category}");
    }
}
public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Processing crypto wallet transfer of amount {transaction.Amount} in the category {transaction.Category}");
    }
}
public class Account
{
    public string AccountNumber;
    public decimal Balance { get; protected set; }
    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }
    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
        Console.WriteLine($"Transaction applied. New balance: {Balance}");
    }
}
public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
     : base(accountNumber, initialBalance) { }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. New balance: {Balance}");
        }
    }
}
public class FinanceApp
{
    private List<Transaction> _transactions = new List<Transaction>();
    public void Run()
    {
        SavingsAccount Account1 = new SavingsAccount("ACC1", 200);
        Transaction transaction1 = new Transaction(1, DateTime.Now, 29, "Groceries");
        Transaction transaction2 = new Transaction(2, DateTime.Now, 12, "Utilities");
        Transaction transaction3 = new Transaction(3, DateTime.Now, 5, "Sports");

        ITransactionProcessor momoProcessor = new MobileMoneyProcessor();
        ITransactionProcessor bankProcessor = new BankTransferProcessor();
        ITransactionProcessor cryptoProcessor = new CryptoWalletProcessor();

        momoProcessor.Process(transaction1);
        bankProcessor.Process(transaction2);
        cryptoProcessor.Process(transaction3);

        Account1.ApplyTransaction(transaction1);
        Account1.ApplyTransaction(transaction2);
        Account1.ApplyTransaction(transaction3);

        _transactions.AddRange(new[] { transaction1, transaction2, transaction3 });
        Console.WriteLine("Transaction history:");
        foreach (var transaction in _transactions)
        {
            Console.WriteLine($"Transaction ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount}, Category: {transaction.Category}");
        }
    }
}