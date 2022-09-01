using Server_DataModels;

namespace server.DTOs
{
    public class DTO_TRANSACTION_RequestCreate
    {
        // FIELDS
        public int request_from { get; set; }
        public int org_account { get; set; }
        public double amount { get; set; }
        public bool request_type { get; set; }
        public string? request_note { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_RequestCreate() { }

        public DTO_TRANSACTION_RequestCreate(int request_from, int org_account, double amount, bool request_type, string? request_note)
        {
            this.request_from = request_from;
            this.org_account = org_account;
            this.amount = amount;
            this.request_type = request_type;
            this.request_note = request_note;
        }
    }
}
