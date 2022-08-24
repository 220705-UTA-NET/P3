using server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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

        public async Task<Customer> GetCustomerAsync(int id)
        {

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Customer WHERE customer_id=@CustomerId;";

            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@CustomerId", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();

            int customerid = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            string email = reader.GetString(3);
            int phonenumber = reader.GetInt32(4);
            string password = reader.GetString(5);

            Customer result = new(customerid, firstname, lastname, email, phonenumber, password);

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetCustomerAsync, returned {0}.", result.Id);

            return result;
        }
        public async Task UpdateCustomerAsync(int id, string email, int phoneNumber, string password)
        {
            string cmdText = "UPDATE Customer SET email = @email,phone = @phone,password = @password Where customer_id = @id";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phoneNumber);
            cmd.Parameters.AddWithValue("@password", password);

            await connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
    }
}
