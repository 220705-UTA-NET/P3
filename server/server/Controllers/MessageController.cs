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
        private readonly Brass_IRepository _repo;
        private readonly ILogger<MessageController> _logger;
        public MessageController(Brass_IRepository repo, ILogger<MessageController> logger) {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetAllTickets() {
            IEnumerable<TicketDTO> tickets;
            try
            {
                tickets = await _repo.LoadAllTickets();
                _logger.LogInformation($"loaded all tickets ...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }

            return tickets.ToList();
        }


        [HttpGet("/message/{key}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetAllMessagesbyTicket(string? key = null)
        {
            IEnumerable<MessageDTO> messages;
            try
            {
                messages = await _repo.LoadAllMessagesbyTicket(key);
                _logger.LogInformation($"loaded messages for #{key}  # ...");
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
            
            return messages.ToList();
        }

        [HttpPut("/ticket/{ticketid}")]
        public async Task<ActionResult> UpdateTicket(string ticketid)
        {
            try
            {
                await _repo.UpdateTicket(ticketid);
                _logger.LogInformation($"User #{ticketid} status changed # ...");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                // Minor error checking for now
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
