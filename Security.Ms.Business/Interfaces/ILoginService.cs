using Security.Ms.Domain.Dto;

namespace Security.Ms.Business.Interfaces
{
    public interface ILoginService
    {
        Task<UserModel?> Authenticate(UserLogin login);
        string GenerateToken(UserModel user);
    }
}
