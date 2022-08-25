using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub: Hub
    {
        // where should this function be used?
        public Task SendMessage1(string user, string message)
        {
            Console.WriteLine("SendMessage1 in hub");
            string clientId = Context.ConnectionId;
            Console.WriteLine(clientId, user, message);
            return Clients.Client(clientId).SendAsync("ReceiveOne", user, message, "in the hub!");     
        }
    }
}
