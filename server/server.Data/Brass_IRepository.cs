using server.Model;

namespace server.Data
{
    public interface Brass_IRepository
    {
        //Adds a tech support user
        public Task AddSupport(SupportDTO support);

        // adds a ticket
        public Task AddTicket(TicketDTO ticket);

        // adds a message and attaches it to aticket
        public Task AddMessage(MessageDTO message);

        //gets ticket history 
        public Task<List<TicketDTO>> LoadAllTickets();

        //gets all message from a particular ticket/ chatroomID
        public Task<List<MessageDTO>> LoadAllMessagesbyTicket(string chatRoomId);

        //gets all tickets from a particular user //

        // update ticket status
        public Task UpdateTicket(string ticketid);
        //
    }
}