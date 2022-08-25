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
            return Clients.Group("techSupport").SendAsync("joinTech", "Joined tech support chat...");
        }

        // place user into their private room & inform techSupport
        public Task OpenTicket(int privateRoomKey)
        {
            string clientId = Context.ConnectionId;
            Groups.AddToGroupAsync(clientId, privateRoomKey.ToString());

            return Clients.Group("techSupport").SendAsync("OpenTicket", privateRoomKey);
        }

        // TECH SUPPORT ONLY 
        // will be used to connect an indiviudal tech support staff to a user ticket
        public Task TechSupportJoinsConversation(int privateRoomKey)
        { 
            // Id of the tech support
            string connectionId = Context.ConnectionId;
            // add tech support to private room
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", "Tech support has joined the chat");
        }

        public Task SendChat(string user, string message, int privateRoomKey)
        {
            Console.WriteLine("send chat");
            string clientId = Context.ConnectionId;

            Console.WriteLine(clientId, user, message);

            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", user, message, "in the hub!");
        }

        // other methods we will need: 
        // broadcast to all support (if we take the approach outlined above)
        // remove all support but the person who responds
        // message just the people in this particular group
    }
}
