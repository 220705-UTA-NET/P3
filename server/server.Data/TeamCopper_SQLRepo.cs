using Microsoft.Extensions.Logging;
using server.Model;
using System.Data.SqlClient;

namespace server.Data
{
    public class TeamCopper_SQLRepo : TeamCopper_IRepo
    {
        private readonly string _connectionString;
        private readonly ILogger<TeamCopper_SQLRepo> _logger;

        public TeamCopper_SQLRepo(string connectionString, ILogger<TeamCopper_SQLRepo> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<Customer> getCustomerByEmail(string email)
        {
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmd = "SELECT customer_id, first_name, last_name, username, phone " +
                           " FROM [project3].[Customer] " +
                           " WHERE email = @email;";

            using SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@email", email);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            int customer_id = reader.GetInt32(0);
            string first = reader.GetString(1);
            string last = reader.GetString(2);
            string username = reader.GetString(3);
            string phone = reader.GetString(4);

            Customer customer = new Customer(customer_id, first, last, email, username, phone);

            await connection.CloseAsync();

            return customer;
        }

        public async Task<Customer> getCustomerByUserName(string username)
        {
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmd = "SELECT customer_id, first_name, last_name, email, phone " +
                           " FROM [project3].[Customer] " +
                           " WHERE username = @username;";

            using SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@username", username);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            int customer_id = reader.GetInt32(0);
            string first = reader.GetString(1);
            string last = reader.GetString(2);
            string email = reader.GetString(3);
            string phone = reader.GetString(4);

            Customer customer = new Customer(customer_id, first, last, email, username, phone);

            await connection.CloseAsync();

            return customer;
        }

        public async Task<Customer> customerLogInAsync(string username, string password)
        {
            Customer customer = new Customer();

            using SqlConnection connection = new(_connectionString);


            await connection.OpenAsync();

            string cmd = @"SELECT customer_id, username, password FROM [project3].[Customer] WHERE username = @username AND password = @password;";

            SqlCommand command = new SqlCommand(cmd, connection);


            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);


            using SqlDataReader reader = command.ExecuteReader();

            if (await reader.ReadAsync())
            {
                customer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }

            return customer;
        }

        public async Task<Customer> registerCustomerAsync(string FirstName, string LastName, string UserName, string Email, string Phone, string Password)
        {
            Customer customer = new Customer(FirstName, LastName, UserName, Email, Phone, Password);
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmd = "INSERT INTO [project3].[Customer] (first_name, last_name, username, email, phone, password) " +
                            "VALUES( @first_name, @last_name, @username, @email, @phone, @password);";

            using SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@first_name", FirstName);
            command.Parameters.AddWithValue("@last_name", LastName);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@username", UserName);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@password", Password);

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            return customer;
        }
        public async Task<Support> getSupportByEmail(string email)
        {
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmd = "SELECT support_id, first_name, last_name, username, phone " +
                           " FROM [project3].[Support] " +
                           " WHERE email = @email;";

            using SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@email", email);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            int support_id = reader.GetInt32(0);
            string first = reader.GetString(1);
            string last = reader.GetString(2);
            string username = reader.GetString(3);
            string phone = reader.GetString(4);

            Support support = new Support(support_id, first, last, email, username, phone);

            await connection.CloseAsync();

            return support;
        }

        public async Task<Support> getSupportByUserName(string username)
        {
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmd = "SELECT support_id, first_name, last_name, email, phone " +
                           " FROM [project3].[Support] " +
                           " WHERE username = @username;";

            using SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@username", username);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            int support_id = reader.GetInt32(0);
            string first = reader.GetString(1);
            string last = reader.GetString(2);
            string email = reader.GetString(3);
            string phone = reader.GetString(4);

             Support support = new Support(support_id, first, last, email, username, phone);
            
            await connection.CloseAsync();

            return support;
        }

        public async Task<Support> supportLogInAsync(string username, string password)
        {
            Support support = new Support();

            using SqlConnection connection = new(_connectionString);


            await connection.OpenAsync();

            string cmd = @"SELECT support_id, username, password FROM [project3].[Support] WHERE username = @username AND password = @password;";

            SqlCommand command = new SqlCommand(cmd, connection);


            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);


            using SqlDataReader reader = command.ExecuteReader();

            if (await reader.ReadAsync())
            {
                support = new Support(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
            
            return support;
        }

        //public async Task<Support> registerSupportAsync(string FirstName, string LastName, string UserName, string Email, string Phone, string Password)
        //{
        //    Support support = new Support(FirstName, LastName, UserName, Email, Phone, Password);
        //    using SqlConnection connection = new(_connectionString);

        //    await connection.OpenAsync();


        //    string cmd = "INSERT INTO [project3].[Support] (first_name, last_name, username, email, phone, password) " +
        //                    "VALUES( @first_name, @last_name, @username, @email, @phone, @password);";


        //    using SqlCommand command = new(cmd, connection);

        //    command.Parameters.AddWithValue("@first_name", FirstName);
        //    command.Parameters.AddWithValue("@last_name", LastName);
        //    command.Parameters.AddWithValue("@email", Email);
        //    command.Parameters.AddWithValue("@username", UserName);
        //    command.Parameters.AddWithValue("@phone", Phone);
        //    command.Parameters.AddWithValue("@password", Password);

        //    await command.ExecuteNonQueryAsync();

        //    await connection.CloseAsync();

        //    return support;
        //}
    }
}
