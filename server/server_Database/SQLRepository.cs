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

        // METHODS
        public async Task<List<DMODEL_Transaction>> TRANSACTION_ASYNC_getTransactons(int INPUT_AccountNumber)
        {
            // Setting up SQL command 
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_ConnectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT transaction_id, account_id, time, amount, type, completion_status FROM [project3].[Transaction] WHERE account_id = @INPUT_AccountID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_AccountID", INPUT_AccountNumber);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            // Outcome if no transaction history can be found
            if (DB_reader.HasRows == false)
            {
                List<DMODEL_Transaction> OUTPUT_BlankHistory = new List<DMODEL_Transaction>();
                DMODEL_Transaction OUTPUT_DummyTransaction = new DMODEL_Transaction(-1, -1, DateTime.MinValue, -1, false, false);

                OUTPUT_BlankHistory.Add(OUTPUT_DummyTransaction);

                API_PROP_Logger.LogInformation("EXECUTED: TRANSACTION_ASYNC_getTransactons --> OUTPUT: Unable to find transaction history for account id {0}, returning blank list", INPUT_AccountNumber);
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
                    float TEMP_amount = DB_reader.GetFloat(3);
                    bool TEMP_type = DB_reader.GetBoolean(4);
                    bool TEMP_completion_status = DB_reader.GetBoolean(5);

                    DMODEL_Transaction TEMP_Transaction = new DMODEL_Transaction(TEMP_transaction_id, TEMP_account_id, TEMP_time, TEMP_amount, TEMP_type, TEMP_completion_status);

                    OUTPUT_TransactionHistory.Add(TEMP_Transaction);
                }

                API_PROP_Logger.LogInformation("EXECUTED: TRANSACTION_ASYNC_getTransactons --> OUTPUT: Found transaction history for account id {0}, returning list", INPUT_AccountNumber);
                await DB_reader.CloseAsync();
                await DB_connection.CloseAsync();
                return OUTPUT_TransactionHistory;
            }
        }


    }
}
