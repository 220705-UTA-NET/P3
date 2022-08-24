using server.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace server.Data
{
    public class SQLRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<SQLRepository> _logger;
        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            List<Customer> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT customer_id, first_name, last_name, email, phone, password FROM Customers;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

                int customerid = reader.GetInt32(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = reader.GetString(3);
                int phonenumber = reader.GetInt32(4);
                string password =  reader.GetString(5);

                Customer tmpCustomer = new(customerid, firstname ,lastname, email, phonenumber,password);
                result.Add(tmpCustomer);

            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllCustomersAsync, returned {0} results", result.Count);

            return result;
        }
        public async Task<StatusCodeResult> UpdateCustomerAsync(int id, string email, int phonenumber, string password)
        {
            string cmdText = "UPDATE Customer SET email = @email,phone = @phone,password = @password Where customer_id = @id";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phonenumber);
            cmd.Parameters.AddWithValue("@password", password);



            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                _logger.LogError("Error in UpdateCustomer while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed UpdateCustomerAsync");
            return new StatusCodeResult(200);

        }
    }
}
