using Security.Ms.Domain.Dto;

namespace Security.Ms.DataAccess.Interfaces
{
    public interface IUsersDbMock
    {
        List<UserModel> GetUsersDb();
    }
}
