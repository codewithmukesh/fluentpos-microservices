using BuildingBlocks.Constants;
using BuildingBlocks.CQRS;
using BuildingBlocks.EFCore;
using BuildingBlocks.Enums;
using BuildingBlocks.Events;
using BuildingBlocks.Logging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.OpenID;
using BuildingBlocks.Options;
using BuildingBlocks.Swagger;
using BuildingBlocks.Validation;
using BuildingBlocks.Web;
using FluentPOS.Catalog.Application;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var appConfig = builder.Services.GetRequiredConfiguration<AppConfig>();

// Add services to the container.
builder.RegisterSerilog(builder.Environment, appConfig.Name);
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.RegisterValidators(typeof(CatalogRoot).Assembly);
builder.Services.RegisterSwagger(appConfig.Name);

builder.Services.RegisterContext<CatalogDbContext>(builder.Configuration, Database.PostgreSQL, ConnectionStrings.DefaultConnection);
builder.Services.AddScoped<IDataSeeder, ProductDataSeeder>();
builder.Services.RegisterEventBus(builder.Configuration);

//Auth
builder.Services.RegisterJWTAuth();


// Register BB Services
builder.Services.RegisterMediatR(typeof(CatalogRoot).Assembly, enableLoggingBehavior: true);
builder.Services.AddSingleton<ExceptionMiddleware>();
var app = builder.Build();

//
app.ConfigureMigrations<CatalogDbContext>(builder.Environment);
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!").RequireAuthorization();
app.Run();
