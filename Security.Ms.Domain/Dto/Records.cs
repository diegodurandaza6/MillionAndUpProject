namespace Security.Ms.Domain.Dto
{
    public record UserLogin(string UserName, string Password) { }
    public record UserModel(string Identification, string UserName, string Password, string Rol, string Name, string Surname, string Phone, string Email) { }
}