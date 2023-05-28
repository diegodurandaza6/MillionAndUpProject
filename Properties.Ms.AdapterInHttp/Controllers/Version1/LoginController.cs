using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Properties.Ms.AdapterInHttp.Constansts;
using Properties.Ms.AdapterInHttp.Utilities;
using Properties.Ms.Domain.Property.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Properties.Ms.AdapterInHttp.Controllers.Version1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login(UserLogin login)
        {
            UserModel? user = await AuthenticateService(login);
            if (user != null)
            {
                string jwtToken = GenerateToken(user);
                return Ok(new { token = jwtToken });
            }
            return Unauthorized();
            
        }

        //TODO: Crear metodo privados en una clase aparte del controler
        private async Task<UserModel?> AuthenticateService(UserLogin login)
        {
            CipherHelper _helper = new(_configuration);//TODO: Validar dependencia.
            UserModel? currentUser = UsersMock.UsersDb.FirstOrDefault(
                u => u.UserName.ToLower() == login.UserName.ToLower() && _helper.Decrypt(u.Password) == login.Password
            );
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        //TODO: Crear metodo privados en una clase aparte del controler
        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol),
            };
            var secutiryToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(secutiryToken);
        }
    }
}
