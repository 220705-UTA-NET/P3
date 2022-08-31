using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class TicketDTO
    {
        //public int id { get; set; }
        public string chatRoomId { get; set; }

        // Customer Username
        public string user { get; set; }
        public string message { get; set; }

        // ticket status
        public bool open { get; set; }

        //public TicketDTO(int id, string chatRoomId, string username, string message, bool open) { 
        public TicketDTO(string chatRoomId, string username, string message, bool open)
        {
            this.chatRoomId = chatRoomId;
            this.user = username;
            this.message = message;
            this.open = open;
        }
    }
}
