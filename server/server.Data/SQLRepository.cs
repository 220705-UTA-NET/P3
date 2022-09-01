﻿/* 

using Microsoft.Extensions.Logging;
using server.Model;
using System.Data.SqlClient;

namespace server.Data{
    public class SQLRepository : IRepository
    {
        // URL of the Database being used
        private readonly string _ConnectionString;
        // Something to log the actions of our API
        private readonly ILogger<SQLRepository> _logger;

        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            _ConnectionString = connectionString;
            _logger = logger;
        }

        public async Task AddSupport(SupportDTO support)
        {         
            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();
            
            string cmdText =
            @"INSERT INTO Support (first_name, last_name, email, username, phone, password)
            VALUES
            (@first_name, @last_name, @email, @username, @phone, @password)";


            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@first_name", support.first_name);
            cmd.Parameters.AddWithValue("@last_name", support.last_name);
            cmd.Parameters.AddWithValue("@email", support.email);
            cmd.Parameters.AddWithValue("@username", support.username);
            cmd.Parameters.AddWithValue("@phone", support.phone);
            cmd.Parameters.AddWithValue("@password", support.password);

            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            _logger.LogInformation("Executed AddSupport");
        }

        public async Task AddTicket(TicketDTO ticket)
        {
            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();

            string cmdText =
            @"INSERT INTO Ticket (customer_id, ticket_status)
            VALUES
            (@customer_id , @ticket_status)";

            string cmdText2 = "SELECT * FROM Customer WHERE username = @username;";

            SqlCommand cmd2 = new SqlCommand(cmdText2, connection);

            cmd2.Parameters.AddWithValue("@username", ticket.user);

            using SqlDataReader reader = await cmd2.ExecuteReaderAsync();

            int status ;

            if (ticket.open)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            
            cmd.Parameters.AddWithValue("@customer_id", reader.GetInt32(0));
            cmd.Parameters.AddWithValue("@ticket_status", status);
            

            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            _logger.LogInformation("Executed AddTicket");
        }

        public async Task AddMessage(MessageDTO message)
        {
            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();

            string cmdText =
            @"INSERT INTO Message (message_content, message_DateTime, message_user,)
            VALUES
            (@message_content, @message_DateTime, @message_user)";


            SqlCommand cmd = new SqlCommand(cmdText, connection);

            //cmd.Parameters.AddWithValue("@ticket_id", message.ticket_id);
            cmd.Parameters.AddWithValue("@message_content", message.message);
            cmd.Parameters.AddWithValue("@message_DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@message_user", message.user);


            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            _logger.LogInformation("Executed AddMessage");
        }
    
    
    
    
    
    }
}*/