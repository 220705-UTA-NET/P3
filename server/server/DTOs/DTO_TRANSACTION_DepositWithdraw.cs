using Server_DataModels;

namespace server.DTOs
{
    public class DTO_TRANSACTION_DepositWithdraw
    {
        // FIELDS
        public int AccountID { get; set; }
        public double ChangeAmount { get; set; }

        // CONSTRUCTORS
        public DTO_TRANSACTION_DepositWithdraw() { }
        public DTO_TRANSACTION_DepositWithdraw(int AccountID, double ChangeAmount)
        {
            this.AccountID = AccountID;
            this.ChangeAmount = ChangeAmount;
        }
    }
}
