using server.Model;

namespace server.DTOs
{
    public class DTO_TRANSACTION_TransactionHistory
    {
        // FIELDS
        public int NumberOfTransactions { get; set; }
        public IEnumerable<DMODEL_Transaction>? LIST_DMODEL_Transactions { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_TransactionHistory() { }

        public DTO_TRANSACTION_TransactionHistory(int NumberOfTransactions, IEnumerable<DMODEL_Transaction>? LIST_DMODEL_Transactions)
        {
            this.NumberOfTransactions = NumberOfTransactions;
            this.LIST_DMODEL_Transactions = LIST_DMODEL_Transactions;
        }

    }
}
