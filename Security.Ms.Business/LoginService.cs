using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Security.Ms.Business.Interfaces;
using Security.Ms.DataAccess.Interfaces;
using Security.Ms.DataAccess.Utilities;
using Security.Ms.Domain.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Security.Ms.Business
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersDbMock _usersDbMock;

        public LoginService(IConfiguration configuration, IUsersDbMock usersDbMock)
        {
            _configuration = configuration;
            _usersDbMock = usersDbMock;
        }

        /// <summary>
        /// Método para validar credenciales
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Usuario autenticado</returns>
        public async Task<UserModel?> Authenticate(UserLogin login)
        {
            CipherHelper _helper = new(_configuration["Encrypt:Key"], _configuration["Encrypt:IV"]);
            UserModel? currentUser = _usersDbMock.GetUsersDb().FirstOrDefault(
                u => u.UserName.ToLower() == login.UserName.ToLower() && u.Password == _helper.Encrypt(login.Password)
            );
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        /// <summary>
        /// Método para generar el token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token de autorización</returns>
        public string GenerateToken(UserModel user)
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
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiresMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(secutiryToken);
        }
    }
}