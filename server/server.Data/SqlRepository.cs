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

        public Task DeleteBudgetAsync(int budgetId)
        {
            throw new NotImplementedException();
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

        public Task<Budget> InsertBudgetAsync(Budget budget)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "INSERT INTO Budget(budget_id, customer_id, account_id, monthly_id, warning_amount, date) VALUES(@budget, @customer, @account, @monthly, @warning, @date);";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budget.BudgetId);
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                command.Parameters.AddWithValue("@warning", budget.WarningAmount);
                command.Parameters.AddWithValue("@date", budget.Date);

                // get amount
                var result = command.ExecuteScalar();

                // convert to string then int
                Int32.TryParse(result.ToString(), out );

            }


        }

        public Task<Budget> UpdateBudgetAsync(Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
