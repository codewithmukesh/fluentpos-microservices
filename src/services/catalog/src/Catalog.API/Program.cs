using BuildingBlocks.CQRS;
using BuildingBlocks.EFCore;
using BuildingBlocks.Logging;
using FluentPOS.Catalog.Application;
using FluentPOS.Catalog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCommonLogger(builder.Environment);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());
builder.Services.AddEFCoreDbContext<CatalogDbContext>(builder.Configuration, BuildingBlocks.Enums.Database.PostgreSQL);
// Register BB Services
builder.Services.UseCommonMediatR(typeof(CatalogRoot).Assembly, enableLoggingBehavior: true);

var app = builder.Build();

//
app.UseEFCoreMigration<CatalogDbContext>(builder.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.DefaultModelsExpandDepth(-1));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!");
app.Run();
