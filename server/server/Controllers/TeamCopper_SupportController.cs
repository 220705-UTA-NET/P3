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

        public TeamCopper_SupportController(TeamCopper_IRepo repo, ILogger<TeamCopper_SupportController> logger)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/login/support")]
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
                    response.Add("SupportUserName", support.FirstName + " " + support.LastName);


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
    }
}