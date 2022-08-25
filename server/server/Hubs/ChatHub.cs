using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub: Hub
    {
        // in order to connect a user with a support, likely need to use group?
        // if a user submits a ticket, broadcast it to all support?
        // from there, we can remove all support EXCEPT for the person who picks up the ticket
        // once users are in same group, each time someone sends a message, it should go to both (and can thus render in their chat box)
        public Task ConversationStartup(string message)
        {
            string clientId = Context.ConnectionId;
            Console.WriteLine(clientId);
            Groups.AddToGroupAsync(clientId, "techSupport");
            return Clients.Group("techSupport").SendAsync(message);
        }


        public Task SendChat(string user, string message)
        {
            string clientId = Context.ConnectionId;

            Console.WriteLine(clientId, user, message);
            // return Clients.Client(clientId).SendAsync("ReceiveOne", user, message, "in the hub!");
            return Clients.Group("techSupport").SendAsync("ReceiveOne", user, message, "in the hub!");
        }

        // other methods we will need: 
        // broadcast to all support (if we take the approach outlined above)
        // remove all support but the person who responds
        // message just the people in this particular group
    }
}
