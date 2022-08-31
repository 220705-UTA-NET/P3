using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class TicketDTO
    {
        //public string ticketID { get; set; }
        public int id { get; set; }

        // Customer ID
        public int customer_id { get; set; }
        
        // ticket status
        public int ticket_status { get; set; }
        
        public TicketDTO(int id, int customer_id, int ticket_status) { 
            this.id = id;
            this.customer_id = customer_id;
            this.ticket_status = ticket_status;
        }
    }
}
