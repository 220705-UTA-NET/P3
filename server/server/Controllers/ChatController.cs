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


        [Route("send")]
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageDto msg)
        {
            Console.WriteLine("start of sendrequest");
            Console.WriteLine(msg.message);

            // I think we need to decide which client to send this message to? Should this be returned to sender, and/or be sent to the receiver?
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.message, "in the controller");
            return Ok();
        }
    }
}
