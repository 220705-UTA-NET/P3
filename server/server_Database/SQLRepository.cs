using Microsoft.Extensions.Logging;
using Server_DataModels;
using System.Data.SqlClient;

namespace server_Database
{
    public class SQLRepository : IRepository
    {
        // FIELDS
        private readonly string DB_PROP_ConnectionString;
        private readonly ILogger<SQLRepository> API_PROP_Logger;

        // CONSTRUCTORS
        public SQLRepository (string INPUT_ConnectionString, ILogger<SQLRepository> INPUT_Logger)
        {
            this.DB_PROP_ConnectionString = INPUT_ConnectionString;
            this.API_PROP_Logger = INPUT_Logger;
        }

        // ===================================== METHODS ====================================================================================================
        public async Task<List<DMODEL_Transaction>> TRANSACTION_SQL_ASYNC_GetTransactonHistory(int INPUT_AccountNumber)
        {
            // Setting up SQL command 
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT transaction_id, account_id, time, amount, transaction_notes, transaction_type, completion_status 
                                        FROM [project3].[Transaction] WHERE account_id = @INPUT_AccountID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_AccountID", INPUT_AccountNumber);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            // Outcome if no transaction history can be found
            if (DB_reader.HasRows == false)
            {
                List<DMODEL_Transaction> OUTPUT_BlankHistory = new List<DMODEL_Transaction>();
                DMODEL_Transaction OUTPUT_DummyTransaction = new DMODEL_Transaction(-1, -1, DateTime.MinValue, -1, "", false, false);

                OUTPUT_BlankHistory.Add(OUTPUT_DummyTransaction);

                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_ASYNC_getTransactons ({0}) --> OUTPUT: !FAILURE = Unable to find transaction history for account id, returning blank list", INPUT_AccountNumber);

                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_BlankHistory;
            }
            // Outcome if transaction history is found, Parsing data
            else
            {   
                List<DMODEL_Transaction> OUTPUT_TransactionHistory = new List<DMODEL_Transaction>();
                while (await DB_reader.ReadAsync())
                {
                    int TEMP_transaction_id = DB_reader.GetInt32(0);
                    int TEMP_account_id = DB_reader.GetInt32(1);
                    DateTime TEMP_time = DB_reader.GetDateTime(2);
                    double TEMP_amount = DB_reader.GetDouble(3);
                    string TEMP_transaction_notes = DB_reader.GetString(4);
                    bool TEMP_type = DB_reader.GetBoolean(5);
                    bool TEMP_completion_status = DB_reader.GetBoolean(6);

                    DMODEL_Transaction TEMP_Transaction = new DMODEL_Transaction(TEMP_transaction_id, TEMP_account_id, TEMP_time, TEMP_amount, TEMP_transaction_notes, TEMP_type, TEMP_completion_status);

                    OUTPUT_TransactionHistory.Add(TEMP_Transaction);
                }

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_ASYNC_getTransactons ({0}) --> OUTPUT: Found transaction history for account id {0}, returning list", INPUT_AccountNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_TransactionHistory;
            }
        }

        //====================================================================================================================================================

        public async Task<bool> TRANSACTION_SQL_ASYNC_InsertNewTransaction(int INPUT_AccountNumber, double INPUT_ChangeAmount, string INPUT_TransactionNotes, bool INPUT_TransactionType)
        {
            try
            {
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"INSERT INTO [project3].[Transaction] (account_id, time, amount, transaction_notes, transaction_type, completion_status) 
                                            VALUES (@INPUT_AccountID, @INPUT_Time, @INPUT_Amount, @INPUT_transaction_notes, @INPUT_transaction_type, @INPUT_completion_status)";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_AccountID", INPUT_AccountNumber);
                DB_command.Parameters.AddWithValue("@INPUT_Time", DateTime.Now);
                DB_command.Parameters.AddWithValue("@INPUT_Amount", INPUT_ChangeAmount);
                DB_command.Parameters.AddWithValue("@INPUT_transaction_notes", INPUT_TransactionNotes);
                DB_command.Parameters.AddWithValue("@INPUT_transaction_type", INPUT_TransactionType);
                DB_command.Parameters.AddWithValue("@INPUT_completion_status", true);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_SQL_ASYNC_InsertNewTransaction ({0}) --> OUTPUT: Successfully inserted new transaction for account {1}", INPUT_AccountNumber, INPUT_AccountNumber);
                return true;
            }
            catch (Exception ERROR_UnableInsertIntoTransactionTable)
            {
                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_SQL_ASYNC_InsertNewTransaction ({0}) --> OUTPUT: !FAILED = Unable to insert new transaction for account {1}", INPUT_AccountNumber, INPUT_AccountNumber);
                API_PROP_Logger.LogWarning(ERROR_UnableInsertIntoTransactionTable.Message, ERROR_UnableInsertIntoTransactionTable);
                return false;
            }
        }

        //====================================================================================================================================================

        public async Task<double> TRANSACTION_SQL_ASYNC_GetAccountBalance(int INPUT_AccountNumber)
        {
            // Setting up SQL command 
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT [balance] 
                                       FROM [project3].[Account] 
                                       WHERE account_id = @INPUT_AccountID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_AccountID", INPUT_AccountNumber);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            // Outcome if no account balence can be found
            if (DB_reader.HasRows == false)
            {
                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_ASYNC_getAccountBalance ({0}) --> OUTPUT: !FAILURE = Unable to find balence for account id {1}, returning -1", INPUT_AccountNumber, INPUT_AccountNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return -1;
            }
            // Outcome if account balence is found, Parsing data
            else
            {
                await DB_reader.ReadAsync();
               
                double OUTPUT_Balence = DB_reader.GetDouble(0);

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_ASYNC_getTransactons ({0}) --> OUTPUT: Found transaction history for account id {1}, returning list", INPUT_AccountNumber, INPUT_AccountNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_Balence;
            }
        }

        //====================================================================================================================================================

        public async Task<bool> TRANSACTION_SQL_ASYNC_UpdateAccountBalence(int INPUT_AccountNumber, double INPUT_NewBalance)
        {
            try
            {
                // Setting up SQL command 
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"UPDATE [project3].[Account] 
                                            SET [balance] = @INPUT_ChangeAmount
                                            WHERE [account_id] = @INPUT_AccountNumber;";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_AccountID", INPUT_AccountNumber);
                DB_command.Parameters.AddWithValue("@INPUT_ChangeAmount", INPUT_NewBalance);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_ASYNC_updateAccountBalence ({0}, {1}) --> OUTPUT: Updated balence to {2} for account {3}, returning true", INPUT_AccountNumber, INPUT_NewBalance, INPUT_NewBalance, INPUT_AccountNumber);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception ERROR_TRANSACTION_updateBalence)
            {
                API_PROP_Logger.LogError("EXECUTED: TRANSACTION_ASYNC_updateAccountBalence ({0}, {1}) --> OUTPUT: !FAILURE = Unable to update account {2} balence", INPUT_AccountNumber, INPUT_NewBalance, INPUT_AccountNumber);
                API_PROP_Logger.LogError(ERROR_TRANSACTION_updateBalence.Message, ERROR_TRANSACTION_updateBalence);
                return false;
            }
        }

        //====================================================================================================================================================

        public async Task<List<DMODEL_Request>> TRANSACTION_SQL_ASYNC_GetOutstandingRequest(int INPUT_CustomerNumber)
        {
            // Setting up SQL command 
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT request_id, request_from, org_account, amount, req_DatTime, request_type, request_note 
                                        FROM [project3].[Request] WHERE request_from = @INPUT_CustomerNumber;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_CustomerNumber", INPUT_CustomerNumber);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            // Outcome if no transaction history can be found
            if (DB_reader.HasRows == false)
            {
                List<DMODEL_Request> OUTPUT_BlankRequest = new List<DMODEL_Request>();
                DMODEL_Request OUTPUT_DummyRequest = new DMODEL_Request(-1,-1,-1,-1,DateTime.MinValue, false, "");

                OUTPUT_BlankRequest.Add(OUTPUT_DummyRequest);

                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_SQL_ASYNC_GetOutstandingRequest ({0}) --> OUTPUT: !FAILURE = Unable to find requests customer {1}, returning blank list", INPUT_CustomerNumber, INPUT_CustomerNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_BlankRequest;
            }
            // Outcome if transaction history is found, Parsing data
            else
            {
                List<DMODEL_Request> OUTPUT_OutstandingRequest = new List<DMODEL_Request>();
                while (await DB_reader.ReadAsync())
                {
                    int TEMP_request_id = DB_reader.GetInt32(0);
                    int TEMP_request_from = DB_reader.GetInt32(1);
                    int TEMP_org_account = DB_reader.GetInt32(2);
                    double TEMP_amount = DB_reader.GetDouble(3);
                    DateTime TEMP_time = DB_reader.GetDateTime(4);
                    bool TEMP_request_type = DB_reader.GetBoolean(5);
                    string TEMP_request_notes = DB_reader.GetString(6);

                    DMODEL_Request TEMP_Request = new DMODEL_Request(TEMP_request_id, TEMP_request_from, TEMP_org_account, TEMP_amount, TEMP_time, TEMP_request_type, TEMP_request_notes);

                    OUTPUT_OutstandingRequest.Add(TEMP_Request);
                }

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_SQL_ASYNC_GetOutstandingRequest ({0}) --> OUTPUT: Found transaction history for account id {1}, returning list", INPUT_CustomerNumber, INPUT_CustomerNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_OutstandingRequest;
            }
        }

        //====================================================================================================================================================

        public async Task<bool> TRANSACTION_SQL_ASYNC_InsertNewRequest(int INPUT_CustomerID, int INPUT_OriginAccount, double INPUT_ChangeAmount, bool INPUT_RequestType, string? INPUT_RequestNotes)
        {
            try
            {
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"INSERT INTO [project3].[Request] (request_from, org_account, amount, req_DatTime, request_type, request_note) 
                                            VALUES (@INPUT_CustomerID, @INPUT_OriginAccount, @INPUT_ChangeAmount, @INPUT_Time, @INPUT_RequestType, @INPUT_RequestNotes)";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_CustomerID", INPUT_CustomerID);
                DB_command.Parameters.AddWithValue("@INPUT_OriginAccount", INPUT_OriginAccount);
                DB_command.Parameters.AddWithValue("@INPUT_ChangeAmount", INPUT_ChangeAmount);
                DB_command.Parameters.AddWithValue("@INPUT_Time", DateTime.Now);
                DB_command.Parameters.AddWithValue("@INPUT_RequestType", INPUT_RequestType);
                DB_command.Parameters.AddWithValue("@INPUT_RequestNotes", INPUT_RequestNotes);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_SQL_ASYNC_InsertNewRequest (CustomerID: {0}, OriginAccount: {1}) --> OUTPUT: Successfully inserted new request for customer {2}", INPUT_CustomerID, INPUT_OriginAccount, INPUT_CustomerID);
                return true;
            }
            catch (Exception ERROR_UnableInsertIntoRequestTable)
            {
                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_SQL_ASYNC_InsertNewRequest  (CustomerID: {0}, OriginAccount: {1}) --> OUTPUT: !FAILED = Unable to insert new request for customer {2}", INPUT_CustomerID, INPUT_OriginAccount, INPUT_CustomerID);
                API_PROP_Logger.LogWarning(ERROR_UnableInsertIntoRequestTable.Message, ERROR_UnableInsertIntoRequestTable);
                return false;
            }
        }

        //====================================================================================================================================================

        public async Task<bool> TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest(int INPUT_RequestID)
        {
            try
            {
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"DELETE FROM [project3].[Request] WHERE request_id = @INPUT_RequestID";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_RequestID", INPUT_RequestID);


                await DB_command.ExecuteNonQueryAsync();

                API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest ({0}) --> OUTPUT: Successfully deleted request", INPUT_RequestID);
                return true;
            }
            catch (Exception ERROR_UnableInsertIntoRequestTable)
            {
                API_PROP_Logger.LogWarning("EXECUTED: TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest ({0}) --> OUTPUT: !FAILED = Unable to delete request", INPUT_RequestID);
                API_PROP_Logger.LogWarning(ERROR_UnableInsertIntoRequestTable.Message, ERROR_UnableInsertIntoRequestTable);
                return false;
            }
        }

    }
}
