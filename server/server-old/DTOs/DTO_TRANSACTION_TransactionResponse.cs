using server.Model;

namespace server.DTOs
{
    public class DTO_TRANSACTION_TransactionResponse
    {
        // FIELDS
        public int StatusCode { get; set; }
        public double AccountBalance { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_TransactionResponse() { }
        public DTO_TRANSACTION_TransactionResponse(int StatusCode, double AccountBalance)
        {
            this.StatusCode = StatusCode;
            this.AccountBalance = AccountBalance;
        }
    }
}
