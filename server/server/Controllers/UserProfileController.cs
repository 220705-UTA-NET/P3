using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Model;
using server.Data;
using server.DTOs;
using System.Text.Json;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly Bronze_IRepository _repository;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(Bronze_IRepository repository, ILogger<UserProfileController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("/userprofile")]
        public async Task<ActionResult<Customer>> GetUserProfile(int customerId)
        {
            try
            {
                DMODEL_Customer data = await _repository.GetCustomerAsync(customerId);
                Customer customer = new (data.id, data.firstName, data.lastName, data.email, data.phoneNumber, data.password);
                _logger.LogInformation($"Successfully executed GetCustomerAsync for Customer #{customerId}");
                Response.Headers.AccessControlAllowOrigin = "*";
                return customer;
            }catch(Exception e)
            {
                _logger.LogError(e, "An error occured when executing GetCustomerAsync with Customer ID #{customerId} ...", e.Message);
                return StatusCode(500);
            }
        }

        [HttpOptions("/userprofile")]
        public async Task<ActionResult> OptionRequest()
        {
            Response.Headers.AccessControlAllowOrigin = "http://localhost:4200";
            Response.Headers.AccessControlAllowCredentials = "true";
            Response.Headers.AccessControlAllowMethods = "GET, PUT, OPTIONS";
            Response.Headers.AccessControlAllowHeaders = "Origin, Content-Type, Accept";

            return StatusCode(200);
        }

        [HttpPut("/userprofile")]
        public async Task<ActionResult> ModifyUserProfile([FromBody]Customer customer)
        {
            try
            {
                await _repository.UpdateCustomerAsync(customer.id, customer.firstName, customer.lastName, customer.email, customer.phoneNumber, customer.password);
                _logger.LogInformation($"Successfully executed UpdateCustomerAsync for customer #{customer.id}");
                Response.Headers.AccessControlAllowOrigin = "*";
                return StatusCode(201);
            }catch(Exception e)
            {
                _logger.LogError(e, "An error occurred when executing UpdateCustomerAsync ...", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("/userprofile/password")]
        public async Task<ActionResult> ChangePassword([FromBody] string password)
        {
            try
            {
                Console.WriteLine(password);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
