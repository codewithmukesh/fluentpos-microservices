using BuildingBlocks.Constants;
using BuildingBlocks.CQRS;
using BuildingBlocks.EFCore;
using BuildingBlocks.Enums;
using BuildingBlocks.Logging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.WebHostEnvironment;
using FluentPOS.Catalog.Application;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCommonLogger(builder.Environment);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());
builder.Services.AddEFCoreDbContext<CatalogDbContext>(builder.Configuration, Database.PostgreSQL, ConnectionStrings.DefaultConnection);
builder.Services.AddScoped<IDataSeeder, ProductDataSeeder>();
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
    app.UseSwaggerUI(o => o.DefaultModelsExpandDepth(-1));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!");
app.Run();
