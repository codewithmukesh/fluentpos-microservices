using BuildingBlocks.Auth;
using BuildingBlocks.Constants;
using BuildingBlocks.CQRS;
using BuildingBlocks.EFCore;
using BuildingBlocks.Enums;
using BuildingBlocks.Events;
using BuildingBlocks.Logging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.OpenID;
using BuildingBlocks.Validation;
using BuildingBlocks.WebHostEnvironment;
using FluentPOS.Catalog.Application;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Data.Seeders;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Add services to the container.
builder.AddCommonLoging(builder.Environment);
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddValidation(typeof(CatalogRoot).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => { o.EnableAnnotations(); });
builder.Services.AddEFCoreDbContext<CatalogDbContext>(builder.Configuration, Database.PostgreSQL, ConnectionStrings.DefaultConnection);
builder.Services.AddScoped<IDataSeeder, ProductDataSeeder>();
builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddCurrentUser();

//Auth
builder.Services.AddJWT();


// Register BB Services
builder.Services.UseCommonMediatR(typeof(CatalogRoot).Assembly, enableLoggingBehavior: true);
builder.Services.AddSingleton<ExceptionMiddleware>();
var app = builder.Build();

//
app.UseEFCoreMigration<CatalogDbContext>(builder.Environment);
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsDockerDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!").RequireAuthorization();
app.Run();
