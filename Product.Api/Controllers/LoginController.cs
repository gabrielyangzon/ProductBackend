using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product.Api.Extensions;
using Product.DataTypes.Models;
using Product.DataTypes.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Product.Api.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserModel login)
        {
            bool isAuthenticated = Authenticate(login.Name);

            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            var result = GenerateJSONWebToken(login);

            if (!result.IsAutheticate)
            {
                return BadRequest(result.Error);
            }


            return Ok(result);
        }

        private TokenResponse GenerateJSONWebToken(UserModel userInfo)
        {

            string webToken = string.Empty;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                webToken = new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                return new TokenResponse { IsAutheticate = false, Error = ex.Message };
            }

            return new TokenResponse { IsAutheticate = true, Token = webToken };
        }

        private bool Authenticate(string name)
        {

            if (_config["Jwt:Name"] == name)
            {
                return true;
            }

            return false;
        }
    }
}
