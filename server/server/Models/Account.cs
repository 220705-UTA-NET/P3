namespace server.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int Type { get; set; }
        public double Balance { get; set; }
        public int CustomerId { get; set;}
        public Account(int accountid, int type, double balance, int customerid)
        {
            AccountId = accountid;
            Type = type;
            Balance = balance;
            CustomerId = customerid;
        }
    }
}
