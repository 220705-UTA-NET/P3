using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamCopper_CustomerController : ControllerBase
    {
        private readonly ILogger<TeamCopper_CustomerController> _logger;
        private readonly TeamCopper_IRepo _repo;

        public TeamCopper_CustomerController(ILogger<TeamCopper_CustomerController> logger, TeamCopper_IRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/login/customer")]
        public async Task<ActionResult<Dictionary<string, string>>> LogIn()
        {
            Customer customer;
            try
            {
                string Info = Request.Headers.Authorization;

                string EString = Info.Split(' ')[1];


                byte[] data = Convert.FromBase64String(EString);
                string DString = Encoding.UTF8.GetString(data);

                string[] cred = DString.Split(':');

                customer = await _repo.customerLogInAsync(cred[0], cred[1]);
                if (customer.CustomerId != 0)
                {
                    var claims = new[]
{
                    new Claim(JwtRegisteredClaimNames.Sub, $"{customer.CustomerId}")
                };

                    var secretBytes = Encoding.UTF8.GetBytes(JWTConstants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var algorithm = SecurityAlgorithms.HmacSha256;

                    var signingCredentials = new SigningCredentials(key, algorithm);
                    var token = new JwtSecurityToken(
                        JWTConstants.Issuer,
                        JWTConstants.Audience,
                        claims,
                        DateTime.Now,
                        DateTime.Now.AddHours(1),
                        signingCredentials
                        );
                    var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                    Dictionary<string, string> response = new Dictionary<string, string>();
                    response.Add("Access-Token", tokenJson);
                    response.Add("Role", "Customer");
                    response.Add("CustomerId", customer.CustomerId.ToString());
                    response.Add("CustomerName", customer.FirstName + " " + customer.LastName);


                    return response;
                }
                else
                {
                    _logger.LogError("User does not exist in database");
                    return StatusCode(401);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

        }

        [HttpPost("/register/customer")]
        public async Task<ActionResult<Dictionary<string, string>>> Register()
        {
            Customer customer;
            try
            {
                string Info = Request.Headers.Authorization;
                string EString = Info.Split(' ')[1];

                byte[] data = Convert.FromBase64String(EString);
                string DString = Encoding.UTF8.GetString(data);

                string[] cred = DString.Split(':');

                using StreamReader reader = new StreamReader(Request.Body);
                string json = await reader.ReadToEndAsync();

                JsonObject person = (JsonObject)JsonSerializer.Deserialize(json, typeof(JsonObject));
                customer = await _repo.registerCustomerAsync(person["FirstName"].ToString(), person["LastName"].ToString(), cred[0],
                    person["Email"].ToString(), person["Phone"].ToString(), cred[1]);
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, $"{customer.CustomerId}")
                    };

                var secretBytes = Encoding.UTF8.GetBytes(JWTConstants.Secret);
                var key = new SymmetricSecurityKey(secretBytes);
                var algorithm = SecurityAlgorithms.HmacSha256;

                var signingCredentials = new SigningCredentials(key, algorithm);
                var token = new JwtSecurityToken(
                    JWTConstants.Issuer,
                    JWTConstants.Audience,
                    claims,
                    DateTime.Now,
                    // For now, the token will last for a day. Once refresh tokens are included, this will be shorten down.
                    DateTime.Now.AddHours(1),
                    signingCredentials
                    );
                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("Access-Token", tokenJson);
                response.Add("Role", "Customer");
                response.Add("CustomerId", customer.CustomerId.ToString());
                response.Add("CustomerName", customer.FirstName + " " + customer.LastName);

                return response;

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
