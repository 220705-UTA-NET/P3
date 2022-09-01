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
        //public int id { get; set; }

        //ticket ID
        public string ticketId { get; set; }


        // the user who sent the message client or tech support
        public string user { get; set; }

        //the message itself
        public string message { get; set; }

        //time of message
        public DateTime date { get; set; }


        //public MessageDTO(int id, int ticket_id, string? message_content, DateTime message_date, string message_user)

        public MessageDTO(string ticketId, string user, string message, DateTime date)
        {
            //this.id = id;
            this.ticketId = ticketId;
            this.user = user;
            this.message = message;
            this.date = date;
        }
    }
}