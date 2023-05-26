using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Properties.Ms.AdapterInHttp.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerGenCustomized(this IServiceCollection services, string appName)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{appName} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? $"{appName} {description.ApiVersion} is marked as deprecated. Please consider using a newer version." : string.Empty
                    });
                }

                options.DescribeAllParametersInCamelCase();
            });

            return services;
        }

        // Make available all versions of the API in the ListBox
        public static IApplicationBuilder AllowSwaggerToListApiVersions(this WebApplication app, string appName)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{description.ApiVersion} ({appName})");
                }
                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}
