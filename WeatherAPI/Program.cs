using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using System.Text;
using WeatherAPI.Clients;
using WeatherAPI.Engine;
using WeatherAPI.Engine.Middlewares;
using WeatherAPI.Engine.Settings;
using WeatherAPI.Managers;
using WeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Mongosettings>(builder.Configuration.GetSection("Mongosettings"));
builder.Services.Configure<OpenweathermapApisettings>(builder.Configuration.GetSection("OpenweathermapApisettings"));

// Add services to the container.
builder.Services.AddScoped<IWeatherForecastManager, WeatherForecastManager>();
builder.Services.AddSingleton<IDataAccessService, DataAccessService>();
builder.Services.AddSingleton<IMongoDbFactory, MongoDbFactory>();
builder.Services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>()
    .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "apiWithAuthBackend",
            ValidAudience = "apiWithAuthBackend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAru+OlQN/XBpYBDbP2XL7j6ztS2VJx/l7u/FPWgwx5v5gDung319+dh5ze/d9Mspkushi46f66uBUffJfwCb/KYNxS0lcAMezYHcZryy0uuBVUWg8vLiM/89gc2KME0FzjJHC32yTgb+Ddq5bCNFe636ELoPP5N6YiDN9hOo4r/Nz6kQGy66ioFv9kGRrNS1he9qBL2cVt0DjT1YacWQqReFa0R050Qv8vmdRmOWvrz9GT1Vh9oG1QrWD97IXvw9TQJGiVRwNEr8NLR9sN/+bEBCcRy1IWwhjxj6IMzXNppJhA0z2ceS5Bab8nLlOd/kLeBxmvUJa+DuybOkhdSwNPwIDAQAB"))
        };

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocument(builder.Environment, builder.Configuration);
}

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
