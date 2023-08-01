using Polly;
using Polly.Extensions.Http;
using WeatherAPI.Integrators;
using WeatherAPI.Managers;
using WeatherAPI.Middlewares;
using WeatherAPI.Services;
using WeatherAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Mongosettings>(builder.Configuration.GetSection("Mongosettings"));
builder.Services.Configure<OpenweathermapApisettings>(builder.Configuration.GetSection("OpenweathermapApisettings"));

// Add services to the container.
builder.Services.AddScoped<IWeatherForecastManager, WeatherForecastManager>();
builder.Services.AddSingleton<IDataAccessService, DataAccessService>();
builder.Services.AddSingleton<IMongoDbFactory, MongoDbFactory>();
builder.Services.AddHttpClient<IOpenWeatherMapIntegrator, OpenWeatherMapIntegrator>()
    .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
