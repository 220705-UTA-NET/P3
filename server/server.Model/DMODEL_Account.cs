namespace server.Model
{
    public class DMODEL_Account
    {
        public int AccountId { get; set; }
        public int Type { get; set; }
        public double Balance { get; set; }
        public int CustomerId { get; set;}
        public DMODEL_Account(int accountid, int type, double balance, int customerid)
        {
            AccountId = accountid;
            Type = type;
            Balance = balance;
            CustomerId = customerid;
        }
    }
}
