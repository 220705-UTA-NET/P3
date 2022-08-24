namespace Server_DataModels
{
    public class DMODEL_Request
    {
        // FIELDS
        public int request_id           { get; set; }
        public int sender_id            { get; set; }
        public int reciever_id          { get; set; }
        public float amount             { get; set; }
        public DateTime time            { get; set; }
        public int account_sender_id    { get; set; }
        public int account_reciever_id  { get; set; }
        public int status               { get; set; }

        // CONSTRUCTORS
        public DMODEL_Request() { }

        public DMODEL_Request(int request_id, int sender_id, int reciever_id, float amount, DateTime time, int account_sender_id, int account_reciever_id, int status)
        {
            this.request_id = request_id;
            this.sender_id = sender_id;
            this.reciever_id = reciever_id;
            this.amount = amount;
            this.time = time;
            this.account_sender_id = account_sender_id;
            this.account_reciever_id = account_reciever_id;
            this.status = status;
        }

        // METHODS
    }
}