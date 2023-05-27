using Properties.Ms.Domain.Property.Models;

namespace Properties.Ms.AdapterInHttp.Constansts
{
    public class UsersMock
    {
        public static readonly List<UserModel> UsersDb = new()
        {
            new UserModel()
            {
                Identification = Guid.NewGuid().ToString(),
                UserName = "dduran",
                Password = "WrkjMjsYTpbLFAX1UbFn0w==",//Admin123*
                Rol = "admin",
                Name = "Diego",
                Surname = "Duran",
                Phone = "3001234567",
                Email = "dduran@mail.com"
            },
            new UserModel()
            {
                Identification = Guid.NewGuid().ToString(),
                UserName = "fdaza",
                Password = "aIPMFlDgaeSneaNoRRu7nQ==",//Adviser123*
                Rol = "adviser",
                Name = "Fernando",
                Surname = "Daza",
                Phone = "3001234567",
                Email = "fdaza@mail.com"
            }
        };
    }
}
