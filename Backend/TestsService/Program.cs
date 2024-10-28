using Application.UseCases.AttachmentUseCases;
using Application.UseCases.ChangeStatusTestUseCases;
using Application.UseCases.EmployeeUseCases;
using Application.UseCases.EquipmentsUseCases;
using Application.UseCases.GenericUpdateUseCases;
using Application.UseCases.SamplesUseCases;
using Application.UseCases.SpecificationsUseCases;
using Application.UseCases.TestAttachmentsUseCases;
using Application.UseCases.TestChangeStatusTestUseCases;
using Application.UseCases.TestEquipmentsUseCases;
using Application.UseCases.TestEquipmentUseCases;
using Application.UseCases.TestGenericUpdateUseCases;
using Application.UseCases.Testing;
using Application.UseCases.TestRequests;
using Application.UseCases.Tests;
using Application.UseCases.Tests.Builder;
using Application.UseCases.TestSpecificationsUseCases;
using Application.UseCases.TestTechniciansUseCases;
using Application.UseCases.Users;
using AutoMapper;
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
using Presentation.Messages.Test;
using Presentation.Messages.TestRequests;
using Presentation.Messages.Equipments;
using Shared.AutoMap;
using Domain.Service.Shared;
using TestsService.Application.Service;
using Presentation.Messages.DropDowns;
using Presentation.Messages.Employees;
using Presentation.Messages.Tests;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

//await 5 seconds before continue
await Task.Delay(5000);


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

// Configure Kestrel to use a different port
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8082); // Change to an available port
});


Env.Load();

// Leer la variable de entorno
var fDataPhysical = Environment.GetEnvironmentVariable("FDATA_PHYSICAL") ?? "defaultPhysicalValue";

// Leer la variable de entorno
var fDataVirtual = Environment.GetEnvironmentVariable("FDATA_VIRTUAL") ?? "defaultVirtualValue";

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register FDataService with environment variables
builder.Services.AddTransient<IFDataService>(provider =>
{
    var mapper = provider.GetRequiredService<IMapper>();
    return new FDataService(fDataPhysical, fDataVirtual, mapper);
});
// Obtener la cadena de conexión de la clase estática ConnectionStrings
var connectionString = ConnectionStrings.UserServiceConn;

// Registrar la cadena de conexión como un servicio singleton
builder.Services.AddSingleton(connectionString);

// Agregar la configuración del servicio para SQLDbConnect y IConnectionDB
// Registrar SQLDbConnect como la implementación de IAppConnectionDB
builder.Services.AddSingleton<SqlConnection>(provider => new SqlConnection(connectionString));

builder.Services.AddSingleton<IAppConnectionDB, SQLDbConnect>();
builder.Services.AddSingleton<ISQLDbConnect, SQLDbConnect>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Injection


//DB
//builder.Services.AddTransient<ISQLDbConnect, SQLDbConnect>();

builder.Services.AddTransient<ITestRepository, TestRepository>();
builder.Services.AddTransient<ITestRequestRepository, TestRequestsRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddTransient<IChangeStatusTestRepository, ChangeStatusTestRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ISamplesRepository, SamplesRepository>();
builder.Services.AddTransient<ISpecificationsRepository, SpecificationsRepository>();
builder.Services.AddTransient<ITestChangeStatusTestRepository, TestChangeStatusTestRepository>();
builder.Services.AddTransient<ITestAttachmentsRepository,  TestAttachmentsRepository>();
builder.Services.AddTransient<ITestGenericUpdateRepository, TestGenericUpdateRepository>();
builder.Services.AddTransient<ITestSpecificationsRepository, TestSpecificationsRepository>();
builder.Services.AddTransient<IGenericUpdateRepository, GenericUpdateRepository>();
builder.Services.AddTransient<ITestBuilder, TestBuilder>();
builder.Services.AddScoped<ITestTechniciansRepository, TestTechniciansRepository>();
builder.Services.AddScoped<ITestEquipmentsRepository, TestEquipmentsRepository>();
builder.Services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
builder.Services.AddScoped<ISharedService, SharedService>();
builder.Services.AddScoped<IDropDownsService, DropDownsService>();







builder.Services.AddTransient<IMsgService, MsgService>();


//User
builder.Services.AddScoped<GetUserByIdUseCase>(); 

//UseCase 
//Test Request 
builder.Services.AddScoped<GetAllTestRequestUseCase>();
builder.Services.AddScoped<ApproveOrRejectTestRequestUseCase>();
builder.Services.AddScoped<ChangeStatusTestRequestUseCase>();
builder.Services.AddScoped<CreateTestsRequestUseCase>();
builder.Services.AddScoped<GetTestsRequestByStatusUseCase>();
builder.Services.AddScoped<GetTestRequestByIdUseCase>();
builder.Services.AddScoped<UpdateTestRequestUseCase>();
builder.Services.AddScoped<GetTestOfTestRequestsUseCases>();




//Test
builder.Services.AddScoped<AddTestUseCase>(); 
builder.Services.AddScoped<PatchTestUseCase>();
builder.Services.AddScoped<GetAllTestsUseCase>();
builder.Services.AddScoped<GetTestByIdUseCase>();
builder.Services.AddScoped<GetAllTestsByStatusUseCase>();
builder.Services.AddScoped<RemoveProfileFromTestUseCase>();
builder.Services.AddScoped<UpdateDatesOfTestUseCase>();



//Attachments
builder.Services.AddScoped<AddAttachmentUseCase>(); 
builder.Services.AddScoped<UpdateAttachmentUseCase>();
builder.Services.AddScoped<GetAttachmentByIDUseCase>();
builder.Services.AddScoped<RemoveAttachmentUseCase>();

//Test Attachments
builder.Services.AddScoped<GetAttachmentsFromTestUseCase>();
builder.Services.AddScoped<AsignAttachmentToTestUseCase>(); 
builder.Services.AddScoped<RemoveAttachmentFromTestUseCase>();
builder.Services.AddScoped<CreateAndAsignAttachmentUseCase>();


//Samples
builder.Services.AddScoped<AddSampleUseCase>(); 
builder.Services.AddScoped<UpdateSampleUseCase>();
builder.Services.AddScoped<GetSampleByIdUseCase>();
builder.Services.AddScoped<DeleteSampleUseCase>();

//EQUIPMENT

builder.Services.AddScoped<AddEquipmentUseCase>();
builder.Services.AddScoped<UpdateEquipmentUseCase>();
builder.Services.AddScoped<GetEquipmentByIdUseCase>();
builder.Services.AddScoped<DeleteEquipmentUseCase>();
builder.Services.AddScoped<GetAllEquipmentsUseCase>();

//Test Equipment
builder.Services.AddScoped<TestEquipmentsFromTestUseCase>();
builder.Services.AddScoped<AsignEquipmentToTestUseCase>();
builder.Services.AddScoped<RemoveEquipmentsFromTestUseCase>();

//Specifications
builder.Services.AddScoped<AddSpecificationUseCase>(); 
builder.Services.AddScoped<UpdateSpecificationUseCase>();
builder.Services.AddScoped<GetSpecificationByIdUseCase>();
builder.Services.AddScoped<DeleteSpecificationUseCase>();

//Test Specification
builder.Services.AddScoped<AsignSpecificationToTestUseCase>();
builder.Services.AddScoped<RemoveSpecificationsFromTestUseCase>();
builder.Services.AddScoped<TestSpecificationsFromTestUseCase>();

//Chage Status
builder.Services.AddScoped<AddChangeStatusTestUseCase>();
builder.Services.AddScoped<GetChangeStatusTestByIdUseCase>();
builder.Services.AddScoped<UpdateChangeStatusTestUseCase>();
builder.Services.AddScoped<DeleteChangeStatusTestUseCase>();

//Test Change Status
builder.Services.AddScoped<AsignChangeStatusTestToTestUseCase>();
builder.Services.AddScoped<RemoveChangeStatusTestFromTestUseCase>();
builder.Services.AddScoped<ChangeStatusFromTestUseCase>();

//Employee
builder.Services.AddScoped<AddEmployeeUseCase>();
builder.Services.AddScoped<GetEmployeeByIdUseCase>();
builder.Services.AddScoped<UpdateEmployeeUseCase>();
builder.Services.AddScoped<DeleteEmployeeUseCase>();
builder.Services.AddScoped<GetEmployeeByEmployeeNumberUseCase>();
builder.Services.AddScoped<GetAllEmployeesUseCase>();
builder.Services.AddScoped<GetEmployeesByTypeUseCase>();

//Technician
builder.Services.AddScoped<AsignTechnicianToTestUseCase>();
builder.Services.AddScoped<GetTechniciansFromTestsUseCase>();
builder.Services.AddScoped<RemoveTechnicianFromTestUseCase>();




//Generic Update
builder.Services.AddScoped<AddGenericUpdateUseCase>(); 
builder.Services.AddScoped<UpdateGenericUpdateUseCase>();
builder.Services.AddScoped<GetGenericUpdateByIdUseCase>();
builder.Services.AddScoped<DeleteGenericUpdateUseCase>();

//Test Generic Update
builder.Services.AddScoped<AsignGenericUpdateToTestUseCase>();
builder.Services.AddScoped<RemoveGenericUpdateFromTestUseCase>();
builder.Services.AddScoped<GetGenericUpdatesFromTestUseCase>();


//Shared


//var natsUrl = "nats://nats:4222"; // URL del servidor NATS
var natsUrl = "nats://localhost:4222"; // URL del servidor NATS

// Configura tu cliente NATS aquí
builder.Services.AddSingleton<NatsConnection>(ctx =>
{
    var cs =  new NatsConnection(new NatsOpts { Url = natsUrl });
    
    return cs;
});




// Agrega AutoMapper con un perfil de mapeo personalizado
builder.Services.AddAutoMapper(typeof(AutoMapperForApp));

// Configuración de CORS
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


//Message
builder.Services.AddScoped<AddTestMessage>();
builder.Services.AddScoped<GetAllTestMessage>();
builder.Services.AddScoped<PatchTestMessage>();
builder.Services.AddScoped<GetAllTestByStatusMessage>();
builder.Services.AddScoped<GetTestByIdMessage>();
builder.Services.AddScoped<UpdateDatesOfTestMessage>();
builder.Services.AddScoped<AddAttachmentTestMessage>();
builder.Services.AddScoped<CreateAndAsignAttachmentToTestMessage>();


//
builder.Services.AddScoped<ChangeStatusTestMessage>();
builder.Services.AddScoped<DeleteAttachmentFromTestMessage>();
builder.Services.AddScoped<DeleteEquipmentFromTestMessage>();
builder.Services.AddScoped<DeleteProfileFromTestMessage>();
builder.Services.AddScoped<DeleteSpecificationFromTestMessage>();
builder.Services.AddScoped<DeleteTechnicianFromTestMessage>();


builder.Services.AddScoped<AddTestRequestMessage>();
builder.Services.AddScoped<UpdateTestRequestMessage>();
builder.Services.AddScoped<GetAllTestRequestsMessage>();
builder.Services.AddScoped<GetTestsRequestByStatusMessage>();
builder.Services.AddScoped<GetTestRequestByIdMessage>();
builder.Services.AddScoped<ApproveOrRejectTestRequestMessage>();
builder.Services.AddScoped<ChangeStatusTestRequestMessage>();


//Equipments

builder.Services.AddScoped<AddEquipmentMessage>();
builder.Services.AddScoped<PatchEquipmentMessage>();
builder.Services.AddScoped<GetEquipmentByIdMessage>();
builder.Services.AddScoped<GetAllEquipmentsMessage>();
builder.Services.AddScoped<GetEquipmentsStatusDropDownMessage>();
//builder.Services.AddScoped<DeleteEquipmentMessage>();


//Employee
builder.Services.AddScoped<AddEmployeeMessage>();
builder.Services.AddScoped<PatchEmployeeMessage>();
builder.Services.AddScoped<GetAllEmployeesMessage>();
builder.Services.AddScoped<GetEmployeeEmployeeNumberMessage>();
builder.Services.AddScoped<GetEmployeesByTypeMessage>();




var app = builder.Build();


app.UseCors("PolicyCors");
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AddTestMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllTestMessage>();
    scope.ServiceProvider.GetRequiredService<PatchTestMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllTestByStatusMessage>();
    scope.ServiceProvider.GetRequiredService<GetTestByIdMessage>();
    scope.ServiceProvider.GetRequiredService<ChangeStatusTestMessage>();
    scope.ServiceProvider.GetRequiredService<DeleteAttachmentFromTestMessage>();
    scope.ServiceProvider.GetRequiredService<DeleteEquipmentFromTestMessage>();
    scope.ServiceProvider.GetRequiredService<DeleteProfileFromTestMessage>();
    scope.ServiceProvider.GetRequiredService<DeleteSpecificationFromTestMessage>();
    scope.ServiceProvider.GetRequiredService<DeleteTechnicianFromTestMessage>();
    scope.ServiceProvider.GetRequiredService<UpdateDatesOfTestMessage>();
    scope.ServiceProvider.GetRequiredService<AddAttachmentTestMessage>();
    scope.ServiceProvider.GetRequiredService<CreateAndAsignAttachmentToTestMessage>();



    //Equipments
    scope.ServiceProvider.GetRequiredService<AddEquipmentMessage>();
    scope.ServiceProvider.GetRequiredService<PatchEquipmentMessage>();
    scope.ServiceProvider.GetRequiredService<GetEquipmentByIdMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllEquipmentsMessage>();
    scope.ServiceProvider.GetRequiredService<GetEquipmentsStatusDropDownMessage>();

    //scope.ServiceProvider.GetRequiredService<DeleteEquipmentMessage>();



//Test Request
    scope.ServiceProvider.GetRequiredService<AddTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<UpdateTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllTestRequestsMessage>();
    scope.ServiceProvider.GetRequiredService<GetTestsRequestByStatusMessage>();
    scope.ServiceProvider.GetRequiredService<GetTestRequestByIdMessage>();
    scope.ServiceProvider.GetRequiredService<ApproveOrRejectTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<ChangeStatusTestRequestMessage>();

//Employee
    scope.ServiceProvider.GetRequiredService<AddEmployeeMessage>();
    scope.ServiceProvider.GetRequiredService<PatchEmployeeMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllEmployeesMessage>();
    scope.ServiceProvider.GetRequiredService<GetEmployeeEmployeeNumberMessage>();
    scope.ServiceProvider.GetRequiredService<GetEmployeesByTypeMessage>();
    

    
    /*scope.ServiceProvider.GetRequiredService<AddTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<UpdateTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<GetAllTestRequestsMessage>();
    scope.ServiceProvider.GetRequiredService<GetTestsRequestByStatusMessage>();
    scope.ServiceProvider.GetRequiredService<GetTestRequestByIdMessage>();
    scope.ServiceProvider.GetRequiredService<ApproveOrRejectTestRequestMessage>();
    scope.ServiceProvider.GetRequiredService<ChangeStatusTestRequestMessage>();
    */
}




app.Run();


