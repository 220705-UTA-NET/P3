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
    public class TeamCopper_SupportController : ControllerBase
    {
        private readonly ILogger<TeamCopper_SupportController> _logger;
        private readonly TeamCopper_IRepo _repo;

        public TeamCopper_SupportController(ILogger<TeamCopper_SupportController> logger, TeamCopper_IRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/Login")]
        public async Task<ActionResult<Dictionary<string, string>>> LogIn()
        {

            Support support;
            try
            {
                string Info = Request.Headers.Authorization;

                string EString = Info.Split(' ')[1];


                byte[] data = Convert.FromBase64String(EString);
                string DString = Encoding.UTF8.GetString(data);

                string[] cred = DString.Split(':');

                foreach (string s in cred)
                {
                    Console.WriteLine("string " + s);
                }

                support = await _repo.supportLogInAsync(cred[0], cred[1]);
                if (support.SupportId != 0)
                {
                    var claims = new[]
{
                    new Claim(JwtRegisteredClaimNames.Sub, $"{support.SupportId}")
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
                    response.Add("Role", "Support");
                    response.Add("SupportId", support.SupportId.ToString());
                    response.Add("CustomerName", support.UserName);


                    return response;
                }
                else
                {
                    _logger.LogError("User unable to be signed in");
                    return StatusCode(401);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

        }

        [HttpPost("/Register")]
        public async Task<ActionResult<Dictionary<string, string>>> Register()
        {
            Support support;
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
                support = await _repo.registerSupportAsync(person["FirstName"].ToString(), person["LastName"].ToString(), cred[0],
                    person["Email"].ToString(), person["Phone"].ToString(), cred[1]);
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, $"{support.SupportId}")
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
                    DateTime.Now.AddDays(1),
                    signingCredentials
                    );
                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("Access-Token", tokenJson);
                response.Add("Role", "Support");
                response.Add("SupportId", support.SupportId.ToString());
                response.Add("SupportName", support.UserName);

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