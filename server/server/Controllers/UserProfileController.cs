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
        public async Task<ActionResult<Customer>> GetUserProfile(int CustomerId)
        {
            try
            {
                Customer customer = await _repository.GetAllCustomersAsync();
                _logger.LogInformation($"Getting userprofile for Customer #{CustomerId}");
            }catch(Exception e)
            {
                _logger.LogError("An error occured in GetUserProfile ... ", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("/userprofile")]
        public async Task<ActionResult> ModifyUserProfile()
        {
            try
            {
                StreamReader reader = new StreamReader(Response.Body, true);
                Customer customer = (Customer)JsonSerializer.Deserialize(reader.ReadToEnd(), typeof(Customer));

                await _repository.UpdateCustomerAsync(customer.Id, customer.Email, customer.PhoneNumber, customer.Password);
                _logger.LogInformation($"Updated Customer #{customer.Id} profile ... ");
                return StatusCode(201);
            }catch(Exception e)
            {
                _logger.LogError($"An error occurred when modifying a customer's profile", e.Message);
                return StatusCode(500);
            }
        }
    }
}
