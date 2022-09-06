using Microsoft.AspNetCore.Mvc;
using server.Model;
using System.Data.SqlClient;
using Server_DataModels;
using System.Diagnostics;

namespace server.Data {
    public class SQLBudgetRepository : IBudgetRepository
    {

        private readonly string _connectionString;

        public SQLBudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DMODEL_Transaction>> GetAllTransactionsAsync()
        {
            List<DMODEL_Transaction> transactions = new List<DMODEL_Transaction>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // query string
                string queryString = "SELECT * FROM [project3].[Transaction] WHERE time BETWEEN '{budget.StartDate}' AND '10-15-2020';";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    int account_id = reader.GetInt32(1);
                    DateTime date = reader.GetDateTime(2);
                    double amount = reader.GetDouble(3);
                    string notes = reader.GetString(4);
                    bool type = reader.GetBoolean(5);
                    bool status = reader.GetBoolean(6);
                    //DMODEL_Transaction t = new();
                    transactions.Add(new DMODEL_Transaction(id, account_id, date, amount, notes, type, status));
                }

                //await connection.CloseAsync();
            }
            return transactions;
        }



        
        public async Task<int> GetTransactionsSumAsync(Budget budget)
        {
            int sum = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();

                // start and end dates need to change
                string queryString = "SELECT SUM(amount) FROM [project3].[Transaction] WHERE time BETWEEN @sd AND @ed AND account_id=@account AND transaction_type=1;";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@sd", budget.StartDate.ToString("MM/dd/yyyy"));
                //Console.WriteLine(budget.StartDate.ToString("MM/dd/yyyy"));
                command.Parameters.AddWithValue("@ed", budget.StartDate.AddMonths(1).AddDays(-1).Date.ToString("MM/dd/yyyy"));
                //Console.WriteLine(budget.StartDate.AddMonths(1).AddDays(-1).Date.ToString("MM/dd/yyyy"));

                // get amount
                var result = await command.ExecuteScalarAsync();
                Debug.WriteLine(result);

                // convert to string then int
                if (result == null) return -1;
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
                string queryString = "INSERT INTO [project3].[Budget](customer_id, account_id, budget_DateTime, monthly_amount, warning_amount) VALUES(@customer, @account, @time, @monthly, @warning);";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@time", budget.StartDate);
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

            Console.WriteLine(budget.ToString());
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();


                // query string
                string queryString = "UPDATE [project3].[Budget] SET monthly_amount=@monthly, warning_amount=@warning, budget_DateTime=@time WHERE budget_id=@budget AND account_id=@account AND customer_id=@customer;";
               

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@budget", budget.BudgetId);//is this needed (get sent from the frontend?)
                command.Parameters.AddWithValue("@customer", budget.CustomerId);
                command.Parameters.AddWithValue("@account", budget.AccountId);
                command.Parameters.AddWithValue("@time", budget.StartDate);
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

        public async Task<ActionResult> DeleteBudgetAsync(int budgetId)// the sql query might need the account_id as well
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // open a connection
                connection.Open();


                // query string
                string queryString = "DELETE FROM [project3].[Budget] WHERE budget_id=@budget;";
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
                string queryString = "SELECT * FROM [project3].[Budget] WHERE customer_id=@customerId;";

                // create the sql command
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@customerId", customerId);

                SqlDataReader reader = await command.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    int budgetId = reader.GetInt32(0);
                    int custId = reader.GetInt32(1);
                    int account_id = reader.GetInt32(2);
                    DateTime date = reader.GetDateTime(3);
                    double monthly_amount = reader.GetDouble(4);                             
                    double warning_amount = reader.GetDouble(5);

                    //int budgetId, int customerId, int accountId, double monthlyAmount, double warningAmount
                    budgets.Add(new Budget(budgetId, custId, account_id, monthly_amount, warning_amount, date));
                }

                
            }
            return budgets;
        }
    }
}
