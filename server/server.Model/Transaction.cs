namespace server.Model;

public class Transaction
{
    public int Deposit_ID { get; set; }
    public int Account_ID { get; set; }
    public DateTime Time { get; set; }
    public double Amount { get; set; }
    public bool Type { get; set; }

    public Transaction()
    {
    }
    
    public Transaction(int deposit, int accountid, DateTime date, double amount, bool type)
    {
        Deposit_ID = deposit;
        Account_ID = accountid;
        Time = date;
        Amount = amount;
        Type = type;
    }
    
}