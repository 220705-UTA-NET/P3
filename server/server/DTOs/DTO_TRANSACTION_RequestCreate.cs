using Server_DataModels;

namespace server.DTOs
{
    public class DTO_TRANSACTION_RequestCreate
    {
        // FIELDS
        public int reciever_from { get; set; }
        public int org_acct { get; set; }
        public double amount { get; set; }
        public bool request_type { get; set; }
        public string? request_notes { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_RequestCreate() { }

        public DTO_TRANSACTION_RequestCreate(int reciever_from, int org_acct, double amount, bool request_type, string? request_notes)
        {
            this.reciever_from = reciever_from;
            this.org_acct = org_acct;
            this.amount = amount;
            this.request_type = request_type;
            this.request_notes = request_notes;
        }
    }
}
