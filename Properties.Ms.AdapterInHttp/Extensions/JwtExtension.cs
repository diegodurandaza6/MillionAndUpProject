using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Properties.Ms.AdapterInHttp.Extensions
{
    public static class JwtExtension
    {
        public static IServiceCollection AddJwtCustomized(this IServiceCollection services, IConfiguration configuration)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:key").Value));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                };
            });
            return services;
        }
    }
}
