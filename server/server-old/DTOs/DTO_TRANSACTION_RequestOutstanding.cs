using server.Model;

namespace server.DTOs
{
    public class DTO_TRANSACTION_RequestOutstanding
    {
        // FIELDS
        public int NumberOfRequests { get; set; }

        public IEnumerable<DMODEL_Request>? LIST_DMODEL_Request { get; set; } 

        // CONSTRUCTORS
        public DTO_TRANSACTION_RequestOutstanding() { }

        public DTO_TRANSACTION_RequestOutstanding(int NumberOfRequests, IEnumerable<DMODEL_Request>? LIST_DMODEL_Request)
        {
            this.NumberOfRequests = NumberOfRequests;
            this.LIST_DMODEL_Request = LIST_DMODEL_Request;
        }
    }
}
