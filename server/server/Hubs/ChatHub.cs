using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub: Hub
    {
        // where should this function be used?
        public Task SendMessage1(string user, string message)
        {
            string clientId = Context.ConnectionId;   
            return Clients.Client(clientId).SendAsync("ReceiveOne", user, message, "in the hub!");     
        }
    }
}
