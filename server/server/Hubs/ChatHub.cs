using Microsoft.AspNetCore.SignalR;
using server.Model;
using server.Data;

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
        public Task TechSupportJoinsConversation(string chatRoomId)
        { 
            // Id of the tech support
            string connectionId = Context.ConnectionId;
            // add tech support to private room
            Groups.AddToGroupAsync(connectionId, chatRoomId);

            return Clients.Group(chatRoomId).SendAsync("conversationStarted", "Tech support has joined the chat", chatRoomId);
        }

        // USER: create a ticket, open a private room, notify tech
        // chatRoomId is just the client's username
        public async Task OpenTicket(string chatRoomId, MessageDTO initialMessage)
        {
            string connectionId = Context.ConnectionId;
            Groups.AddToGroupAsync(connectionId, chatRoomId);
            return Clients.Group("techSupport").SendAsync("OpenTicket", chatRoomId, initialMessage);
        }

        // BOTH TECH & USER: exchanges messages with both parties in private room
        public Task SendChat(MessageDTO newMessage, string chatRoomId)
        {
            string clientId = Context.ConnectionId;
            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(chatRoomId).SendAsync("messaging", newMessage);
        }

        public async Task SaveChatToDB(MessageDTO message) {
            try
            {
                await Brass_SQLRepository.AddMessage(message);
            }
            catch (Exception e) { }
        }

        public Task CloseTicket(string chatRoomId, MessageDTO finalMessage)
        {
            return Clients.Group(chatRoomId).SendAsync("CloseTicket", finalMessage);
        }
    }
}
