using Server_DataModels;

namespace server_Database
{
    public interface IRepository
    {
        Task<List<DMODEL_Transaction>> TRANSACTION_ASYNC_getTransactons(int INPUT_AccountNumber);
        // FUNCTION:
        //      Gets the transaction history of a specific account
        // PARAMETER (int):
        //      Account's ID
        // OUTPUT (DMODEL_Transaction):
        //      If can find data -> return transaction history
        //          OR
        //      If not data for account found -> return dummy list with first element of (-1, -1, DateTime.Min, -1, false, false)

    }
}