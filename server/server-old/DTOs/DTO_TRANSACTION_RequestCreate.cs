using server.Model;

namespace server.DTOs
{
    public class DTO_TRANSACTION_RequestCreate
    {
        // FIELDS
        public string reciever_email { get; set; }
        public int org_acct { get; set; }
        public double amount { get; set; }
        public bool request_type { get; set; }
        public string? request_notes { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_RequestCreate() { reciever_email = ""; }

        public DTO_TRANSACTION_RequestCreate(string reciever_email, int org_acct, double amount, bool request_type, string? request_notes)
        {
            this.reciever_email = reciever_email;
            this.org_acct = org_acct;
            this.amount = amount;
            this.request_type = request_type;
            this.request_notes = request_notes;
        }
    }
}
