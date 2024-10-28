using Domain.Services;
using Infrastructure.Services;
using Presentation.Services;
using GraphiQl;
using GraphQL.Types;
using Presentation.Types;
using Presentation.Queries;
using Presentation.Schemas;
using GraphQL;
using NATS.Client.Core;
using Presentation.Mutation;
using Presentation.Types.Inputs;
using System.Text.Json.Serialization;
using Serilog;
using System.Diagnostics;
using GraphQL.Upload.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Presentation.Types.Customers;
using Domain.Models;
using Presentation.Types.Customers.Input;

var builder = WebApplication.CreateBuilder(args);

// Iniciar el servidor NATS
var natsProcess = StartNatsServer();

try{
    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()  //minimum level of the log sirve para indicar el nivel minimo de los logs que se van a guardar en este caso es debug entonces se guardaran todos los logs ya que debug es el nivel mas bajo hay otros niveles como information, warning, error, fatal y none  
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();    
    //builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();

}catch(Exception ex){
    Console.WriteLine(ex.Message);
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMsgService, MsgService>();
builder.Services.AddTransient<IUsersMicroService, UsersMicroService>();
builder.Services.AddTransient<ITestsMicroServices, TestsMicroServices>(); // Add the ITestsMicroServices service
builder.Services.AddTransient<ITestRequestService, TestsRequestService>(); // Add the ITestsMicroServices service
builder.Services.AddTransient<IEmployeesService,EmployeesService>();
builder.Services.AddTransient<IEquipmentsMicroServices,EquipmentsService>();
builder.Services.AddTransient<IFDataService,FDataService>();
builder.Services.AddTransient<ICustomerService,CustomerService>();


// Configuración de CORS
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers() 
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Configura tu cliente NATS

  //  var natsUrl = "nats://localhost:4222"; // URL del servidor NATSvar natsUrl = "nats://nats:4222"; // URL del servidor NATS
var natsUrl = "nats://localhost:4222"; // URL del servidor NATS
builder.Services.AddSingleton<NatsConnection>(ctx =>
{
    var cs = new NatsConnection(new NatsOpts { Url = natsUrl });
    return cs;
});

// Registrar tipos de GraphQL
builder.Services.AddTransient<UserType>();
builder.Services.AddTransient<AttachmentType>(); // Register AttachmentType
builder.Services.AddTransient<EmployeeType>(); // Register EmployeeType
builder.Services.AddTransient<SpecificationType>(); // Register SpecificationType
builder.Services.AddTransient<EquipmentType>(); // Register EquipmentType
builder.Services.AddTransient<TestStatusEnumType>(); // Register TestStatusEnumType
builder.Services.AddTransient<EmployeeEnumType>(); // Register EmployeeTypeEnum
builder.Services.AddTransient<TestType>(); // Register TestType
builder.Services.AddTransient<TestRequestType>(); // Register TestRequestType
builder.Services.AddTransient<ChangeStatusTestRequestType>(); // Register ChangeStatusTestRequestType
builder.Services.AddTransient<GenericUpdateType>(); // Register GenericUpdateType
builder.Services.AddTransient<TestRequestsStatusEnumType>(); // Register TestRequestsStatusEnumType
builder.Services.AddTransient<CustomerType>(); // Register CustomerType

// Registrar queries
builder.Services.AddTransient<UserQuery>();
builder.Services.AddTransient<TestQuery>(); // Register TestQuery
builder.Services.AddTransient<TestRequestQuery>();
builder.Services.AddTransient<UserProfileType>();
builder.Services.AddTransient<UserProfileQuery>();
builder.Services.AddTransient<CustomerQuery>(); // Register CustomerQuery
//builder.Services.AddTransient<EmployeeQuery>(); // Register EmployeeQuery

// Registrar mutaciones
builder.Services.AddTransient<UserInputType>();
builder.Services.AddTransient<TestInputType>();
builder.Services.AddTransient<TestRequestInputType>();
builder.Services.AddTransient<GenericUpdateInputType>();
builder.Services.AddTransient<CustomerInputType>();

builder.Services.AddTransient<AttachmentInputType>();
builder.Services.AddTransient<EmployeeInputType>();
builder.Services.AddTransient<SpecificationInputType>();
builder.Services.AddTransient<EquipmentInputType>();
builder.Services.AddTransient<ChangeStatusTestInputType>();



//Others
builder.Services.AddTransient<ChangeStatusTestRequestInputType>();



builder.Services.AddTransient<GenericResponseType>();
builder.Services.AddTransient<UsersMutation>(); // Ensure UserMutation is defined correctly
builder.Services.AddTransient<TestMutation>(); // Register TestMutation
builder.Services.AddTransient<TestRequestMutation>(); // Register TestMutation
builder.Services.AddTransient<CustomerMutation>(); // Register CustomerMutation

// Registrar esquemas
builder.Services.AddTransient<RootQuery>();
builder.Services.AddTransient<RootMutation>();
builder.Services.AddTransient<ISchema, RootSchema>();


builder.Services.AddGraphQL(builder => builder
    .AddAutoSchema<ISchema>()
    .AddSystemTextJson()
   .AddGraphTypes()   
);
builder.Services.AddGraphQLUpload();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors("PolicyCors");

// Configurar GraphiQL y GraphQL
app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();




// Cerrar el servidor NATS cuando la aplicación se cierre
app.Lifetime.ApplicationStopping.Register(() =>
{
    if (natsProcess != null && !natsProcess.HasExited)
    {
        natsProcess.Kill();
        natsProcess.WaitForExit();
        Console.WriteLine("Servidor NATS cerrado.");
    }
});

app.UseAuthorization();
app.MapControllers();
app.Run();


//Nats Server
Process StartNatsServer()
{
    // Verificar si el servidor NATS ya está en ejecución
    var existingNatsProcess = Process.GetProcessesByName("nats-server").FirstOrDefault();
    if (existingNatsProcess != null)
    {
        Console.WriteLine("El servidor NATS ya está en ejecución.");
        return existingNatsProcess;
    }

    // Relative path to the nats-server executable Nats\nats-server.exe
    var natsServerPath = Path.Combine(Directory.GetCurrentDirectory(), "Nats", "nats-server.exe");
    if (!File.Exists(natsServerPath))
    {
        Console.WriteLine($"El archivo {natsServerPath} no fue encontrado.");
        throw new FileNotFoundException("No se encontró el archivo del servidor NATS.", natsServerPath);
    }

    // Configuración del servidor de NATS
    var startInfo = new ProcessStartInfo
    {
        FileName = natsServerPath,
        Arguments = "-p 4222",  // Usar el puerto 4222
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    // Ejecutar el servidor NATS
    var process = new Process { StartInfo = startInfo };
    process.Start();

    Console.WriteLine("Iniciando el servidor NATS...");

    // Esperar para asegurarse de que NATS se ha iniciado correctamente
    Task.Delay(2000).Wait();

    if (!process.HasExited)
    {
        Console.WriteLine("Servidor NATS iniciado correctamente en el puerto 4222.");
        return process;
    }
    else
    {
        Console.WriteLine("Error al iniciar el servidor NATS.");
        throw new Exception("No se pudo iniciar el servidor NATS.");
    }
}