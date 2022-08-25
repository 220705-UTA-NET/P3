using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub: Hub
    {
        // in order to connect a user with a support, likely need to use group?
        // if a user submits a ticket, broadcast it to all support?
        // from there, we can remove all support EXCEPT for the person who picks up the ticket
        // once users are in same group, each time someone sends a message, it should go to both (and can thus render in their chat box)

        // will only be joined by support staff
        public Task JoinSupportChat()
        {
            Console.WriteLine("JoinSupportChat");
            string clientId = Context.ConnectionId;
            Groups.AddToGroupAsync(clientId, "techSupport");
            return Clients.Group("techSupport").SendAsync("Joined tech support chat...");
        }

        // place open ticket into pool of techSupport
        public Task OpenTicket(int privateRoomKey)
        {
            Console.WriteLine("open ticket");
            string clientId = Context.ConnectionId;
            Console.WriteLine(clientId);
            Groups.AddToGroupAsync(clientId, privateRoomKey.ToString());

            return Clients.Group("techSupport").SendAsync("OpenTicket", "new ticket opened", privateRoomKey);
        }

        // will be used to connect a user's ticket with an indiviudal tech support staff
        // also need to remove user from techSupport first and foremost
        // public Task ConversationStartup()
        // {

        // }

        public Task SendChat(string user, string message, int privateRoomKey)
        {
            Console.WriteLine("send chat");
            string clientId = Context.ConnectionId;

            Console.WriteLine(clientId, user, message);

            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(privateRoomKey.ToString()).SendAsync("ReceiveOne", user, message, "in the hub!");
        }

        // other methods we will need: 
        // broadcast to all support (if we take the approach outlined above)
        // remove all support but the person who responds
        // message just the people in this particular group
    }
}
