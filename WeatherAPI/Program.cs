using WeatherAPI;
using WeatherAPI.Integrators;
using WeatherAPI.Managers;
using WeatherAPI.Middlewares;
using WeatherAPI.Services;
using WeatherAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Mongosettings>(builder.Configuration.GetSection("Mongosettings"));
builder.Services.Configure<OpenweathermapApisettings>(builder.Configuration.GetSection("OpenweathermapApisettings"));

//builder.Services.AddSingleton<BooksService>();
builder.Services.AddScoped<IWeatherForecastManager, WeatherForecastManager>();
builder.Services.AddScoped<IOpenweathermapIntegrator, OpenweathermapIntegrator>();
builder.Services.AddSingleton<IMongoDBService, MongoDBService>();

// Add services to the container.

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
