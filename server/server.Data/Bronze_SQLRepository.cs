using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using server.Model;


namespace server.Data
{
    public class Bronze_SQLRepository : Bronze_IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<Bronze_SQLRepository> _logger;

        public Bronze_SQLRepository(string connectionString, ILogger<Bronze_SQLRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<DMODEL_Customer> GetCustomerAsync(int id)
        {
            using SqlConnection connection = new(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM project3.Customer WHERE customer_id=@CustomerId;";

            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@CustomerId", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();

            int customerid = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            string email = reader.GetString(3);
            string phonenumber = reader.GetString(4);
            string password = reader.GetString(5);

            DMODEL_Customer result = new(customerid, firstname, lastname, email, phonenumber, password);

            await connection.CloseAsync();

            return result;
        }
        public async Task UpdateCustomerAsync(int id, string firstName, string lastName, string email, string phoneNumber, string password)
        {
            string cmdText = "UPDATE project3.Customer SET first_name=@firstName, last_name=@LastName, email = @email,phone = @phone,password = @password Where customer_id = @id";
            SqlConnection connection = new(this._connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phoneNumber);
            cmd.Parameters.AddWithValue("@password", password);

            await connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<IEnumerable<DMODEL_Account>> GetCustomerAccountsAsync(int id)
        {
            List<DMODEL_Account> result = new();
            using SqlConnection connection = new(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM project3.Account WHERE customer_id=@CustomerId;";

            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@CustomerId", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

                int accountid = reader.GetInt32(0);
                int type = reader.GetInt32(1);
                double balance = reader.GetDouble(2);
                int customerid = reader.GetInt32(3);


                DMODEL_Account tempAccount = new(accountid, type, balance, customerid);
                result.Add(tempAccount);
            }

            await connection.CloseAsync();

            return result;
        }

        public async Task<DMODEL_Account> AddAccountAsync(int customerId, int accountType)
        {
            using SqlConnection connection = new(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "INSERT INTO project3.Account OUTPUT INSERTED.* VALUES (0, 0.00, @CustomerId);";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();

            DMODEL_Account result = new(reader.GetInt32(0), reader.GetInt32(1), reader.GetDouble(2), reader.GetInt32(3));

            reader.Close();
            connection.Close();

            return result;

        }
    }
}
