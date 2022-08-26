using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub: Hub
    {
        // TECH: joins techSupport, enabling notifications 
        public Task JoinSupportChat()
        {
            string clientId = Context.ConnectionId;
            Groups.AddToGroupAsync(clientId, "techSupport");
            return Clients.Group("techSupport").SendAsync("joinTech", "Joined tech support chat...");
        }

        // TECH: will be used to connect an indiviudal tech support staff to a user ticket
        public Task TechSupportJoinsConversation(int privateRoomKey)
        { 
            Console.WriteLine("connectionIDNext");
            // Id of the tech support
            string connectionId = Context.ConnectionId;
            // add tech support to private room
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", "Tech support has joined the chat");
        }

        // USER: create a ticket, open a private room, notify tech
        public Task OpenTicket(int privateRoomKey)
        {
            string clientId = Context.ConnectionId;
            Groups.AddToGroupAsync(clientId, privateRoomKey.ToString());

            return Clients.Group("techSupport").SendAsync("OpenTicket", privateRoomKey);
        }

        // BOTH TECH & USER: exchanges messages with both parties in private room
        public Task SendChat(string user, string message, int privateRoomKey)
        {
            Console.WriteLine("send chat");
            string clientId = Context.ConnectionId;

            Console.WriteLine(clientId, user, message);

            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", user, message, "in the hub!");
        }
    }
}
