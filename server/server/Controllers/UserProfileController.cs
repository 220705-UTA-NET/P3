using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using System.Text.Json;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IRepository repository, ILogger<UserProfileController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("/userprofile")]
        public async Task<ActionResult<Customer>> GetUserProfile(int customerId)
        {
            try
            {
                Customer customer = await _repository.GetCustomerAsync(customerId);
                _logger.LogInformation($"Successfully executed GetCustomerAsync for Customer #{customerId}");
                return customer;
            }catch(Exception e)
            {
                _logger.LogError(e, "An error occured when executing GetCustomerAsync with Customer ID #{customerId} ...", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("/userprofile")]
        public async Task<ActionResult> ModifyUserProfile()
        {
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string result = await reader.ReadToEndAsync();
                Customer customer = (Customer)JsonSerializer.Deserialize(result, typeof(Customer));

                await _repository.UpdateCustomerAsync(customer.Id, customer.Email, customer.PhoneNumber, customer.Password);
                _logger.LogInformation($"Scucessfully executed UpdateCustomerAsync for customer #{customer.Id}");
                return StatusCode(201);
            }catch(Exception e)
            {
                _logger.LogError(e, "An error occurred when executing UpdateCustomerAsync ...", e.Message);
                return StatusCode(500);
            }
        }
    }
}
