using Microsoft.AspNetCore.Mvc;
using server.Model;
using System.Data.SqlClient;


namespace server.Data {
    public class SQLBudgetRepository : IBudgetRepository
    {

        private readonly string _connectionString;

        public SQLBudgetRepository(string connectionString)
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
                string queryString = "SELECT * FROM Transactions WHERE time BETWEEN '{budget.StartDate}' AND '10-15-2020';";

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



        //public async Task<int> GetTransactionsSumAsync(MinusRemainder min)
        public async Task<int> GetTransactionsSumAsync(Budget budget)
        {
            int sum = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // start and end dates need to change
                string queryString = "SELECT SUM(amount) FROM Transactions WHERE time BETWEEN @sd AND @ed AND @account=account_id AND type=0;";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@sd", budget.StartDate.ToString("MM/dd/yyyy"));
                //Console.WriteLine(budget.StartDate.ToString("MM/dd/yyyy"));
                command.Parameters.AddWithValue("@ed", budget.StartDate.AddMonths(1).AddDays(-1).Date.ToString("MM/dd/yyyy"));
                //Console.WriteLine(budget.StartDate.AddMonths(1).AddDays(-1).Date.ToString("MM/dd/yyyy"));

                // get amount
                var result = await command.ExecuteScalarAsync();

                // convert to string then int
                if (result == null) return -1;
                Int32.TryParse(result.ToString(), out sum);
                budget.Remaining = budget.MonthlyAmount - sum;
                Console.WriteLine(budget.Remaining);

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
                string queryString = "INSERT INTO Budgets(customer_id, account_id, monthly_amount, warning_amount, time) VALUES(@customer, @account, @monthly, @warning, @time);";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                command.Parameters.AddWithValue("@warning", budget.WarningAmount);
                command.Parameters.AddWithValue("@time", budget.StartDate);

       
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

            Console.WriteLine(budget.ToString());
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();


                // query string
                string queryString = "UPDATE Budgets SET monthly_amount=@monthly, warning_amount=@warning, time=@time WHERE budget_id=@budget AND account_id=@account AND customer_id=@customer;";
               

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budget.BudgetId);//is this needed (get sent from the frontend?)
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@monthly", budget.MonthlyAmount);
                command.Parameters.AddWithValue("@warning", budget.WarningAmount);
                command.Parameters.AddWithValue("@time", budget.StartDate);


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
                string queryString = "DELETE FROM Budgets WHERE budget_id=@budget;";
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


        public async Task<IEnumerable<Budget>> GetAllBudgetsFromCustomerAsync(int customerId)
        {
            List<Budget> budgets = new List<Budget>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "SELECT * FROM Budgets WHERE @customerId=customer_id;";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@customerId", customerId);

                SqlDataReader reader = await command.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    int budgetId = reader.GetInt32(0);
                    int custId = reader.GetInt32(1);
                    int account_id = reader.GetInt32(2);
                    double monthly_amount = reader.GetDouble(3);                             
                    double warning_amount = reader.GetDouble(4);
                    DateTime date = reader.GetDateTime(5);

                    //int budgetId, int customerId, int accountId, double monthlyAmount, double warningAmount
                    budgets.Add(new Budget(budgetId, custId, account_id, monthly_amount, warning_amount, date));
                }

                
            }
            return budgets;
        }
    }
}
