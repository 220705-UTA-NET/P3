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
        // OUTPUT (List<DMODEL_Transaction>):
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

        Task<List<DMODEL_Request>> TRANSACTION_SQL_ASYNC_GetOutstandingRequest(int INPUT_CustomerNumber);
        // FUNCTION:
        //      Gets the outstanding requests for the customer
        // PARAMETER (int):
        //      Customer's ID
        //  OUTPUT (List<DMODEL_Request>)
        //      If customer has outstanding requests -> return list of requests
        //          OR
        //      If customer has no outstanding requests -> return dummy list with first element of (-1, -1, -1, -1, DateTime.Min(), false, "")

        Task<bool> TRANSACTION_SQL_ASYNC_InsertNewRequest(int INPUT_CustomerID, int INPUT_OriginAccount, double INPUT_ChangeAmount, bool INPUT_RequestType, string? INPUT_RequestNotes);
        // FUNCTION:
        //      Insert new row into request table
        // PARAMETER (int, int, double, bool, string)
        //      Customer's ID
        //      Account ID of Request Originator
        //      Change in amount
        //      Request Type
        //      Request Notes (NULLABLE)
        // OUTPUT (bool)
        //      If successfully insert into [Request] table -> return true
        //          OR
        //      If unable to insert into table -> return false

        Task<bool> TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest(int INPUT_RequestID);
        // FUNCTION:
        //      Deletes Outstanding Request from request table
        // PARAMETER (int)
        //      Request ID
        // OUTPUT (bool)
        //      If successfully delete from [Request] table -> return true
        //          OR
        //      If unable to delete from table -> return false

        Task<int> TRANSACTION_SQL_ASYNC_GetCustomerIDFromEmail(string INPUT_CustomerEmail);
        // FUNCTION:
        //      Gets the Customer ID from an inputed email
        // PARAMETER (int)
        //      Customer Email
        // OUTPUT (bool)
        //      If able to find ID based on email -> return customer id
        //          OR
        //      If unable to find ID -> return -1
    }
}