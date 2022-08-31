// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class MessageDTO
    {
        //message ID
        public int id { get; set; }

        //ticket ID
        public int ticket_id { get; set; }

        //the message itself
        public string? message_content { get; set; }

        //time of message
        public DateTime message_date { get; set; }

        // the user who sent the message client or tech support
        public string message_user { get; set; }

        
        public MessageDTO(int id, int ticket_id, string? message_content, DateTime message_date, string message_user)
        {
            this.id = id;
            this.ticket_id = ticket_id;
            this.message_content = message_content;
            this.message_date = message_date;
            this.message_user = message_user;
        }
    }
}