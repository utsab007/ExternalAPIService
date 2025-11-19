using ThirdPartyApiDemo.Http;
using ThirdPartyApiDemo.Services;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace ThirdPartyApiDemo
{
    [ExcludeFromCodeCoverage]
    public static partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services
            builder.Services.AddHttpClient(); // Needed for HttpClient
            builder.Services.AddTransient<IHttpClientHelper, HttpClientHelper>();
            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ThirdPartyApiDemo", Version = "v1" });
            });

            var app = builder.Build();

            app.UseSwagger(); // Ensure Swagger middleware is available
            app.UseSwaggerUI(); // Ensure Swagger UI middleware is available

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}