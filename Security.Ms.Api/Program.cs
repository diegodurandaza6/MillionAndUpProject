using Microsoft.AspNetCore.Mvc;
using Security.Ms.Business;
using Security.Ms.Business.Interfaces;
using Security.Ms.DataAccess;
using Security.Ms.DataAccess.Interfaces;
using Security.Ms.Domain.Dto;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IUsersDbMock, UsersDbMock>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/token", 
    async ([FromBody, Required] UserLogin login, 
    ILoginService loginService) =>
{
    try
    {
        if (login.UserName == null || login.Password == null)
        {
            return Results.BadRequest("Se requiere un objeto válido en el cuerpo de la solicitud.");
        }
        UserModel? user = await loginService.Authenticate(login);
        if (user != null)
        {
            string jwtToken = loginService.GenerateToken(user);
            return Results.Ok(new { token = jwtToken });
        }

        return Results.Unauthorized();
    }
    catch (Exception)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});

app.Run();
