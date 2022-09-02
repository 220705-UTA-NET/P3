namespace Server_DataModels
{
    public class DMODEL_Request
    {
        // FIELDS
        public int request_id { get; set; }
        public int reciever_from { get; set; }
        public int org_acct { get; set; }
        public double amount { get; set; }
        public DateTime req_DateTime { get; set; }
        public bool request_type { get; set; }
        public string? request_notes { get; set; }

        // CONSTRUCTORS
        public DMODEL_Request() { }
        public DMODEL_Request(int request_id, int reciever_from, int org_acct, double amount, DateTime req_DateTime, bool request_type, string? request_notes)
        {
            this.request_id = request_id;
            this.reciever_from = reciever_from;
            this.org_acct = org_acct;
            this.amount = amount;
            this.req_DateTime = req_DateTime;
            this.request_type = request_type;
            this.request_notes = request_notes;
        }

        // METHODS
    }
}