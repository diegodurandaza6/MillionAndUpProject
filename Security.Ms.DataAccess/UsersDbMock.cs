
using Security.Ms.DataAccess.Interfaces;
using Security.Ms.Domain.Dto;

namespace Security.Ms.DataAccess
{
    public class UsersDbMock : IUsersDbMock
    {
        /// <summary>
        /// Metodo mock para el proceso de validación del login
        /// </summary>
        /// <returns>Listado de usuarios</returns>
        public List<UserModel> GetUsersDb() { 
            return new()
            {
                new(
                    Guid.NewGuid().ToString(),
                    "dduran",
                    "WrkjMjsYTpbLFAX1UbFn0w==",//Admin123*
                    "admin",
                    "Diego",
                    "Duran",
                    "3001234567",
                    "dduran@mail.com"
                ),
                new(
                    Guid.NewGuid().ToString(),
                    "fdaza",
                    "aIPMFlDgaeSneaNoRRu7nQ==",//Adviser123*
                    "adviser",
                    "Fernando",
                    "Daza",
                    "3001234567",
                    "fdaza@mail.com"
                )
            };
        }
    }
}
