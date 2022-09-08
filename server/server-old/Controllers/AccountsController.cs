using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Model;
using server.Data;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly Bronze_IRepository _repository;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(Bronze_IRepository repository, ILogger<AccountsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpOptions("/accounts")]
        public async Task<ActionResult> OptionRequest()
        {
            Response.Headers.AccessControlAllowOrigin = "http://localhost:4200";
            Response.Headers.AccessControlAllowCredentials = "true";
            Response.Headers.AccessControlAllowMethods = "GET, PUT, OPTIONS";
            Response.Headers.AccessControlAllowHeaders = "Origin, Content-Type, Accept";

            return StatusCode(200);
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
        public async Task<ActionResult<Account>> AddAccount(int customerId, int accountType)
        {
            try
            {
                
                DMODEL_Account output = await _repository.AddAccountAsync(customerId, accountType);
                Account result = new Account(output.AccountId, output.Type, output.Balance, output.CustomerId);
                Response.Headers.AccessControlAllowOrigin = "*";

                _logger.LogInformation($"Successfully executed AddAccounts for Customer #{customerId}");
                return result;

            }catch(Exception e)
            {
                _logger.LogError(e, $"An error occured when executing AddAccountAsync with Customer ID #{customerId} ...", e.Message);
                return StatusCode(500);
            }
        }

    }
}
