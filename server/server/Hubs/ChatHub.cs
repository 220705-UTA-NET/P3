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
            Console.WriteLine("privateRoomKey IN TECHSUPPORTJOIN");
            Console.WriteLine(privateRoomKey);
            // Id of the tech support
            string connectionId = Context.ConnectionId;
            // add tech support to private room
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group(privateRoomKey.ToString()).SendAsync("conversationStarted", "Tech support has joined the chat", privateRoomKey.ToString());
        }

        // USER: create a ticket, open a private room, notify tech
        public Task OpenTicket(int privateRoomKey)
        {
            Console.WriteLine("privateRoomKey IN OPEN TICKET");
            Console.WriteLine(privateRoomKey);
            string connectionId = Context.ConnectionId;
            Groups.AddToGroupAsync(connectionId, privateRoomKey.ToString());

            return Clients.Group("techSupport").SendAsync("OpenTicket", privateRoomKey.ToString());
        }

        // BOTH TECH & USER: exchanges messages with both parties in private room

        // USER IS CURRENTLY NOT GETTING MESSAGE SENT BY TECH, BUT TECH IS GETTING MESSAGE SENT BY USER **
        // REASON IS THAT THE ROOM KEY IS NOT AVAILABLE TO THE TECH SUPPORT
        // SAVE IT TO THE SEND BUTTON
        public Task SendChat(string user, string message, int privateRoomKey)
        {
            string clientId = Context.ConnectionId;
            Console.WriteLine("SEND CHAT");
            Console.WriteLine(privateRoomKey);

            // message will need to go to the client's group, should pass it from frontend
            return Clients.Group(privateRoomKey.ToString()).SendAsync("messaging", user, message, "in the hub!");
        }
    }
}
