using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;
using server.Model;

namespace server.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // [Route("send")]
        // [HttpPost]
        // public IActionResult SendRequest([FromBody] MessageDto msg)
        // {
        //     // the below may not be necessary at all, since we can now directly access the hub
        //     _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.message, "in the controller");
        //     ChatHub chatHub = new ChatHub();
        //     chatHub.SendChat(msg.user, msg.message);
        //     return Ok();
        // }
    }
}
