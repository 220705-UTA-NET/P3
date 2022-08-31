namespace Server_DataModels
{
    public class DMODEL_Request
    {
        // FIELDS
        public int request_id { get; set; }
        public int request_from { get; set; }
        public int org_account { get; set; }
        public double amount { get; set; }
        public DateTime req_DatTime { get; set; }
        public bool request_type { get; set; }
        public string? request_note { get; set; }

        // CONSTRUCTORS
        public DMODEL_Request() { }
        public DMODEL_Request(int request_id, int request_from, int org_account, double amount, DateTime req_DatTime, bool request_type, string? request_note)
        {
            this.request_id = request_id;
            this.request_from = request_from;
            this.org_account = org_account;
            this.amount = amount;
            this.req_DatTime = req_DatTime;
            this.request_type = request_type;
            this.request_note = request_note;
        }

        // METHODS
    }
}