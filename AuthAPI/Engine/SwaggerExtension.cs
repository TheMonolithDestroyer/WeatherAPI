using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AuthAPI.Engine
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AuthAPI API v1",
                    Version = "v1",
                    Description = "A v1 WebAPI for managing AuthAPI services"
                });
            });
        }

        public static void UseSwaggerDocument(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((openApiDocument, httpRequest) =>
                {
                    openApiDocument.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "http://localhost:5069" } };
                });
            });

            app.UseSwaggerUI(options => { options.DocExpansion(DocExpansion.None); });
        }
    }
}
