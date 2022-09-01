using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_Database;
using server.DTOs;
using Server_DataModels;

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
                List<DMODEL_Account> result = (List<DMODEL_Account>)(await _repository.GetCustomerAccountsAsync(customerId));

                foreach(DMODEL_Account account in result)
                {
                    accounts.Add(new(account.AccountId, account.Type, account.Balance, account.CustomerId));
                }

                _logger.LogInformation($"Successfully executed GetAccounts for Customer #{customerId}");
                Response.Headers.AccessControlAllowOrigin = "*";
                return accounts;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An error occured when executing GetAccountsAsync with Customer ID #{customerId} ...", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("/accounts")]
        public async Task<ActionResult> AddAccount([FromBody]Account account)
        {
            return StatusCode(201);
        }

    }
}
