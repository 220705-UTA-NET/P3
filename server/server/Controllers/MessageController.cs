using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using server.Data;
using server.Model;
namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IRepository _repo;
        private readonly ILogger<MessageController> _logger;
        public MessageController(IRepository repo, ILogger<MessageController> logger) {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetAllTickets() {
            IEnumerable<TicketDTO> tickets;
            try
            {
                tickets = await _repo.LoadAllTickets();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }

            return tickets.ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetAllMessagesbyTicket(string key = null)
        {
            IEnumerable<MessageDTO> messages;
            try
            {
                messages = await _repo.LoadAllMessagesbyTicket(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }

            return messages.ToList();
        }
    }
}
