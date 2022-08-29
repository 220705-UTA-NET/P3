using Microsoft.AspNetCore.Mvc;
using server.Model;
using System.Data.SqlClient;


namespace server.Data {
    public class SqlRepository : IRepository
    {

        private readonly string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "SELECT * FROM Transactions WHERE time BETWEEN '10-10-2020' AND '10-15-2020';";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    int account_id = reader.GetInt32(1);
                    DateTime date = reader.GetDateTime(2);
                    double amount = reader.GetDouble(3);
                    bool type = reader.GetBoolean(4);

                    transactions.Add(new Transaction(id, account_id, date, amount, type));
                }

                //await connection.CloseAsync();
            }
            return transactions;
        }



        public async Task<int> GetTransactionsSumAsync()
        {
            int sum = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "SELECT SUM(amount) FROM Transactions WHERE time BETWEEN '10-10-2020' AND '10-15-2020';";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                
                // get amount
                var result = command.ExecuteScalar();

                // convert to string then int
                Int32.TryParse(result.ToString(), out sum);

            }
            return sum;
        }

        public async Task<ActionResult> InsertBudgetAsync(Budget budget)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "INSERT INTO Budget(budget_id, customer_id, account_id, monthly_amount, warning_amount) VALUES(@budget, @customer, @account, @monthly, @warning);";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budget.BudgetId);
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                command.Parameters.AddWithValue("@warning", budget.WarningAmount);


                try
                {
                    await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new StatusCodeResult(400);// is this the correct error code?
                }

            }

            return new StatusCodeResult(200);// maybe change this
        }

        public async Task<ActionResult> UpdateBudgetAsync(Budget budget)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();


                // query string
                string queryString = "UPDATE Budget SET @budget=budget_id, @customer=customer_id, @account=account_id, @monthly=monthly_amount, @warning=warning_amount WHERE @budget=budget_id AND @account=account_id;";
               

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budget.BudgetId);
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                command.Parameters.AddWithValue("@warning", budget.WarningAmount);


                try
                {
                    await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new StatusCodeResult(500);// is this the correct error code?
                }

            }

            return new StatusCodeResult(200);// maybe change this
        }

        public async Task<ActionResult> DeleteBudgetAsync(int budgetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();


                // query string
                string queryString = "DELETE FROM Budget WHERE @budget=budget_id;";
                //, @customer=customer_id, @account=account_id, @monthly=monthly_amount, @warning=warning_amount;";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budgetId);
                //command.Parameters.AddWithValue("@customer", budget.CustomerId);
                //command.Parameters.AddWithValue("@account", budget.AccountId);
                //command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                //command.Parameters.AddWithValue("@warning", budget.WarningAmount);


                try
                {
                    await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new StatusCodeResult(500);// is this the correct error code?
                }

            }

            return new StatusCodeResult(200);// maybe change this
        }
    }
}
