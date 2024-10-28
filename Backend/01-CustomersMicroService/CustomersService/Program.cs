using Application.CustomerUseCases;
using Application.UseCases.CustomersUseCases;
using Application.UseCases.CustomerUseCases;
using Domain.DataBase;
using Domain.Repositories;
using Domain.Services;
using DotNetEnv;
using Infrastructure.Config;
using Infrastructure.Persitence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using NATS.Client.Core;
using Presentation.Messages.Customers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

await Task.Delay(5000);


// Configure logging with Serilog
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()  //minimum level of the log sirve para indicar el nivel minimo de los logs que se van a guardar en este caso es debug entonces se guardaran todos los logs ya que debug es el nivel mas bajo hay otros niveles como information, warning, error, fatal y none  
.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();     
builder.Logging.AddSerilog();


// Load environment variables from .env file
Env.Load();

//get nats url from environment variables
var natsUrl = Env.GetString("NATS_URL");


// Configura tu cliente NATS aquí
builder.Services.AddSingleton<NatsConnection>(ctx =>
{
    var cs =  new NatsConnection(new NatsOpts { Url = natsUrl });
    
    return cs;
});



//Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Db
// Obtener la cadena de conexión de la clase estática ConnectionStrings
var connectionString = ConnectionStrings.UserServiceConn;

// Registrar la cadena de conexión como un servicio singleton
builder.Services.AddSingleton(connectionString);

// Agregar la configuración del servicio para SQLDbConnect y IConnectionDB
// Registrar SQLDbConnect como la implementación de IAppConnectionDB
builder.Services.AddSingleton<SqlConnection>(provider => new SqlConnection(connectionString));

builder.Services.AddSingleton<IAppConnectionDB, SQLDbConnect>();
builder.Services.AddSingleton<ISQLDbConnect, SQLDbConnect>();


//Dependency Injection for repositories
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();


//Dependency Injection for services

builder.Services.AddTransient<IMsgService, MsgService>();


//Dependency Injection for UseCases
builder.Services.AddScoped<CreateCustomerUseCase>();
builder.Services.AddScoped<GetCustomerByIdUseCase>();
builder.Services.AddScoped<GetAllCustomersUseCase>();
builder.Services.AddScoped<UpdateCustomerUseCase>();
builder.Services.AddScoped<DeleteCustomerUseCase>();
builder.Services.AddScoped<GetCustomerByCustomerIDUseCase>();


//Message
builder.Services.AddScoped<AddCustomerMessage>();
builder.Services.AddScoped<PatchCustomerMessage>();
builder.Services.AddScoped<GetAllCustomersMessage>();
builder.Services.AddScoped<GetCustomerByIdMessage>();
builder.Services.AddScoped<GetCustomerByCustomerIdMessage>();




// Configuración de CORS
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("PolicyCors");
app.UseHttpsRedirection();


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AddCustomerMessage>();
    scope.ServiceProvider.GetRequiredService<PatchCustomerMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllCustomersMessage>();
    scope.ServiceProvider.GetRequiredService<GetCustomerByIdMessage>();
    scope.ServiceProvider.GetRequiredService<GetCustomerByCustomerIdMessage>();
}




app.Run();









/*

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/
