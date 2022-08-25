namespace Server_DataModels
{
    public class DMODEL_Transaction
    {
        // FIELDS
        public int transaction_id { get; set; }
        public int account_id { get; set; }
        public DateTime time { get; set; }
        public double amount { get; set; }
        public bool type { get; set; }
        public bool completion_status { get; set; }

        // CONSTRUCTORS
        public DMODEL_Transaction() { }

        public DMODEL_Transaction(int transaction_id, int account_id, DateTime time, double amount, bool type, bool completion_status)
        {
            this.transaction_id = transaction_id;
            this.account_id = account_id;
            this.time = time;
            this.amount = amount;
            this.type = type;
            this.completion_status = completion_status;
        }

        // METHODS
    }
}