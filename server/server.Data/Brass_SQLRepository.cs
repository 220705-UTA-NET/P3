/* */

using Microsoft.Extensions.Logging;
using server.Model;
using System.Data.SqlClient;

namespace server.Data{
    public class Brass_SQLRepository : Brass_IRepository
    {
        // URL of the Database being used
        private readonly string _ConnectionString;
        // Something to log the actions of our API
        private readonly ILogger<Brass_SQLRepository> _logger;

        public Brass_SQLRepository(string connectionString, ILogger<Brass_SQLRepository> logger)
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
            @"INSERT INTO Ticket (ticket_id ,customer_id, ticket_status)
            VALUES
            (@ticket_id ,@customer_id , @ticket_status)";

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

            cmd.Parameters.AddWithValue("@ticket_id ", ticket.chatRoomId);
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
            @"INSERT INTO Message (ticket_id,message_content, message_DateTime, message_user,)
            VALUES
            (@ticket_id,@message_content, @message_DateTime, @message_user)";


            SqlCommand cmd = new SqlCommand(cmdText, connection);

            
            cmd.Parameters.AddWithValue("@ticket_id", message.ticketId);
            cmd.Parameters.AddWithValue("@message_content", message.message);
            cmd.Parameters.AddWithValue("@message_DateTime", message.date);
            cmd.Parameters.AddWithValue("@message_user", message.user);


            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            _logger.LogInformation("Executed AddMessage");
        }
           
        public async Task<List<TicketDTO>> LoadAllTickets()
        {
            List<TicketDTO> ticketList = new List<TicketDTO>();

            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * from Ticket;";

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
            
            string cmdText2 = "SELECT username FROM Customer WHERE customer_id = @id;";

            SqlCommand cmd2 = new SqlCommand(cmdText2, connection);
            


            TicketDTO NewTicket;
            while (await reader.ReadAsync())
            {//bring in user inmstead of customer ID
                

                cmd2.Parameters.AddWithValue("@id", reader.GetString(1));

     
                using SqlDataReader reader2 = await cmd2.ExecuteReaderAsync();

                NewTicket = new TicketDTO(reader.GetString(0),reader2.GetString(0), reader.GetString(2), reader.GetBoolean(3));

                ticketList.Add(NewTicket);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetTickets, returned {0} results", ticketList.Count);

            return ticketList;
        }

        public async Task<List<MessageDTO>> LoadAllMessagesbyTicket(string chatRoomId)
        {
            List<MessageDTO> messages = new List<MessageDTO>();

            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Message WHERE ticket_id = @ticket_id;";

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@ticket_id", chatRoomId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();


            MessageDTO message;
            while (await reader.ReadAsync())
            {
                message = new MessageDTO(reader.GetString(1), reader.GetString(4),
                                    reader.GetString(2), reader.GetDateTime(3));

                messages.Add(message);
            }

            _logger.LogInformation("Executed GetMessages, returned {0} results", messages.Count);

            return messages;
        }

        public async Task UpdateTicket(string ticketid)
        {
            using SqlConnection connection = new(_ConnectionString);
            await connection.OpenAsync();

            string cmdText = "UPDATE Ticket SET ticket_status = @value WHERE ticket_id = @ticket_id; ";

            string cmdText2 = "SELECT ticket_status FROM Ticket WHERE ticket_id = @ticket_id;";

            int status;

            SqlCommand cmd2 = new SqlCommand(cmdText2, connection);

            cmd2.Parameters.AddWithValue("@ticket_id", ticketid);

            using SqlDataReader reader = await cmd2.ExecuteReaderAsync();


            if (reader.GetBoolean(0))
            {
                status = 0;
            }
            else
            {
                status = 1;
            }

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@value", status);
            cmd.Parameters.AddWithValue("@ticket_id", ticketid);


            _logger.LogInformation("Executed UpdateTicket");
        }


    }
}