using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class MessageDto
    {
        //public string ticketID { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        //public DateTime date { get; set; }
        public MessageDto(string user, string message) { 
            this.user = user;
            this.message = message;
        }
    }
}
