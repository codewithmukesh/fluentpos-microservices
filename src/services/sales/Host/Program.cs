using BuildingBlocks.Configs;
using BuildingBlocks.Constants;
using BuildingBlocks.CQRS;
using BuildingBlocks.EFCore;
using BuildingBlocks.Enums;
using BuildingBlocks.Events;
using BuildingBlocks.Logging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.OpenID;
using BuildingBlocks.Swagger;
using BuildingBlocks.Validation;
using BuildingBlocks.Web;
using Sales;
using Sales.Data;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var appConfig = builder.Services.GetRequiredConfiguration<AppConfig>();

// Add services to the container.
builder.RegisterSerilog(builder.Environment, appConfig.Name);
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.RegisterValidators(typeof(SalesRoot).Assembly);
builder.Services.RegisterSwagger(appConfig.Name);

builder.Services.RegisterContext<SaleDbContext>(builder.Configuration, Database.PostgreSQL, ConnectionStrings.DefaultConnection);
builder.Services.RegisterEventBus(builder.Configuration);

//Auth
builder.Services.RegisterJWTAuth();


// Register BB Services
builder.Services.RegisterMediatR(typeof(SalesRoot).Assembly, enableLoggingBehavior: true);
builder.Services.AddSingleton<ExceptionMiddleware>();
var app = builder.Build();

//
app.ConfigureMigrations<SaleDbContext>(builder.Environment);
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Sales Service!").RequireAuthorization();
app.Run();