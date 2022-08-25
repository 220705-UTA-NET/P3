using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IRepository repository, ILogger<AccountsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("/accounts")]
        public async Task<ActionResult<List<Account>>> GetAccounts(int customerId)
        {
            try
            {
                List<Account> accounts = new List<Account>();
                accounts =(List<Account>)(await _repository.GetCustomerAccountsAsync(customerId));

                _logger.LogInformation($"Successfully executed GetAccounts for Customer #{customerId}");
                return accounts;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An error occured when executing GetAccountsAsync with Customer ID #{customerId} ...", e.Message);
                return StatusCode(500);
            }
        }
    }
}
