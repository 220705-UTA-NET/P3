using Microsoft.AspNetCore.SignalR;
using server.Model;
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
            // Id of the tech support
            string connectionId = Context.ConnectionId;
            // add tech support to private room
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group(privateRoomKey.ToString()).SendAsync("conversationStarted", "Tech support has joined the chat", privateRoomKey.ToString());
        }

        // USER: create a ticket, open a private room, notify tech
        public Task OpenTicket(int privateRoomKey, MessageDTO initialMessage)
        {
            string connectionId = Context.ConnectionId;
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group("techSupport").SendAsync("OpenTicket", privateRoomKey.ToString(), initialMessage);
        }

        // BOTH TECH & USER: exchanges messages with both parties in private room
        public Task SendChat(MessageDTO newMessage, int privateRoomKey)
        {
            string clientId = Context.ConnectionId;
            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", newMessage);
        }

        public Task CloseTicket(string chatRoomId, MessageDTO finalMessage)
        {
            return Clients.Group(chatRoomId).SendAsync("CloseTicket", finalMessage);
        }
    }
}
