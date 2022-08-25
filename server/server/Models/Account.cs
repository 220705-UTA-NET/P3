namespace server.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public float Balance { get; set; }
        public int CustomerId { get; set;}
        public Account(int accountid, string type, float balance, int customerid)
        {
            AccountId = accountid;
            Type = type;
            Balance = balance;
            CustomerId = customerid;
        }
    }
}
