using Server_DataModels;

namespace server.DTOs
{
    public class DTO_TRANSACTION_RequestResponse
    {
        // FIELDS
        public bool ApprovedTransaction { get; set; }
        public int SelectedAccountID { get; set; }
        public DMODEL_Request RequestData { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_RequestResponse() 
        {
            RequestData = new DMODEL_Request();
        }
        public DTO_TRANSACTION_RequestResponse(bool ApprovedTransaction, int SelectedAccountID, DMODEL_Request RequestData)
        {
            this.ApprovedTransaction = ApprovedTransaction;
            this.SelectedAccountID = SelectedAccountID;
            this.RequestData = RequestData;
        }
    }
}
