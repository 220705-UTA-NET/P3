using Server_DataModels;

namespace server_Database
{
    public interface IRepository
    {
        Task<List<DMODEL_Transaction>> TRANSACTION_SQL_ASYNC_GetTransactonHistory(int INPUT_AccountNumber);
        // FUNCTION:
        //      Gets the transaction history of a specific account
        // PARAMETER (int):
        //      Account's ID
        // OUTPUT (DMODEL_Transaction):
        //      If can find data -> return transaction history
        //          OR
        //      If not data for account found -> return dummy list with first element of (-1, -1, DateTime.Min, -1, false, false)

        Task<bool> TRANSACTION_SQL_ASYNC_InsertNewTransaction(int INPUT_AccountNumber, double INPUT_ChangeAmount, string INPUT_TransactionNotes, bool INPUT_TransactionType);
        // FUNCTION:
        //      Insert new row into transactions table
        // PARAMETER (int, double, string, bool)
        //      Account's ID
        //      Change in balance
        //      Transaction Notes
        //      Transaction Type
        // OUTPUT (bool)
        //      If successfully insert into [Transaction] table -> return true
        //          OR
        //      If unable to insert into table -> return false

        Task<double> TRANSACTION_SQL_ASYNC_GetAccountBalance(int INPUT_AccountNumber);
        // FUNCTION:
        //      Get the current balence of the account
        // PARAMETER (int):
        //      Account's ID
        // OUTPUT (double):
        //      If can find account balence -> return balence
        //          OR
        //      If can't find account -> returns -1

        Task<bool> TRANSACTION_SQL_ASYNC_UpdateAccountBalence(int INPUT_AccountNumber, double INPUT_NewBalance);
        // FUNCTION:
        //      Updates the currently balence to inputed one
        // PARAMETER (int, double):
        //      Account's ID
        //      New Balance 
        // OUTPUT (bool):
        //      If the balence is succesfully updated -> return true
        //          OR
        //      If unable to update balence of account -> return false
    }
}